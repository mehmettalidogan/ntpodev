# OpenCV Model Dosyalarını İndirme Script'i
# PowerShell ile çalıştırın: .\ModelIndir.ps1

Write-Host "OpenCV Model Dosyalarini Indirme Araci" -ForegroundColor Green
Write-Host "=======================================" -ForegroundColor Green
Write-Host ""

$modelKlasoru = "models"

# Models klasörünü oluştur
if (-not (Test-Path $modelKlasoru)) {
    New-Item -ItemType Directory -Path $modelKlasoru | Out-Null
    Write-Host "Models klasoru olusturuldu." -ForegroundColor Yellow
}

# Haar Cascade dosyalarını indir
Write-Host ""
Write-Host "1. Haar Cascade dosyalari indiriliyor..." -ForegroundColor Cyan

$haarURL = "https://raw.githubusercontent.com/opencv/opencv/master/data/haarcascades/"
$haarDosyalari = @(
    "haarcascade_frontalface_default.xml",
    "haarcascade_eye.xml",
    "haarcascade_smile.xml",
    "haarcascade_fullbody.xml"
)

foreach ($dosya in $haarDosyalari) {
    $hedef = Join-Path $modelKlasoru $dosya
    if (Test-Path $hedef) {
        Write-Host "  [ATLA] $dosya (zaten mevcut)" -ForegroundColor Yellow
    } else {
        try {
            Write-Host "  [INDIR] $dosya..." -ForegroundColor Green
            Invoke-WebRequest -Uri ($haarURL + $dosya) -OutFile $hedef
            Write-Host "  [TAMAM] $dosya indirildi" -ForegroundColor Green
        } catch {
            Write-Host "  [HATA] $dosya indirilemedi: $_" -ForegroundColor Red
        }
    }
}

# Proje klasörüne de kopyala
foreach ($dosya in $haarDosyalari) {
    $kaynak = Join-Path $modelKlasoru $dosya
    if (Test-Path $kaynak) {
        Copy-Item $kaynak -Destination "." -Force
        Write-Host "  $dosya proje klasorune kopyalandi" -ForegroundColor Gray
    }
}

Write-Host ""
Write-Host "2. YOLO modeli (otomatik indirme)" -ForegroundColor Cyan

$yoloWeights = Join-Path $modelKlasoru "yolov3-tiny.weights"
$yoloCfg = Join-Path $modelKlasoru "yolov3-tiny.cfg"

if (Test-Path $yoloWeights) {
    Write-Host "  [ATLA] yolov3-tiny.weights (zaten mevcut)" -ForegroundColor Yellow
} else {
    Write-Host "  [INDIR] yolov3-tiny.weights (33 MB)..." -ForegroundColor Green
    Write-Host "  Bu biraz zaman alabilir..." -ForegroundColor Yellow
    try {
        Invoke-WebRequest -Uri "https://pjreddie.com/media/files/yolov3-tiny.weights" -OutFile $yoloWeights
        Write-Host "  [TAMAM] yolov3-tiny.weights indirildi" -ForegroundColor Green
    } catch {
        Write-Host "  [HATA] Indirme basarisiz: $_" -ForegroundColor Red
    }
}

if (Test-Path $yoloCfg) {
    Write-Host "  [ATLA] yolov3-tiny.cfg (zaten mevcut)" -ForegroundColor Yellow
} else {
    Write-Host "  [INDIR] yolov3-tiny.cfg..." -ForegroundColor Green
    try {
        Invoke-WebRequest -Uri "https://raw.githubusercontent.com/pjreddie/darknet/master/cfg/yolov3-tiny.cfg" -OutFile $yoloCfg
        Write-Host "  [TAMAM] yolov3-tiny.cfg indirildi" -ForegroundColor Green
    } catch {
        Write-Host "  [HATA] Indirme basarisiz: $_" -ForegroundColor Red
    }
}

# Proje klasörüne de kopyala
if (Test-Path $yoloWeights) {
    Copy-Item $yoloWeights -Destination "." -Force
    Write-Host "  yolov3-tiny.weights proje klasorune kopyalandi" -ForegroundColor Gray
}
if (Test-Path $yoloCfg) {
    Copy-Item $yoloCfg -Destination "." -Force
    Write-Host "  yolov3-tiny.cfg proje klasorune kopyalandi" -ForegroundColor Gray
}

Write-Host ""
Write-Host "3. TensorFlow modeli (opsiyonel - manuel indirme)" -ForegroundColor Cyan
Write-Host "  TensorFlow Object Detection API modelleri icin:" -ForegroundColor Yellow
Write-Host "  1. http://download.tensorflow.org/models/object_detection/ adresine gidin" -ForegroundColor White
Write-Host "  2. ssd_mobilenet_v3_large_coco_* dosyasini indirin" -ForegroundColor White
Write-Host "  3. Zip'i acin ve frozen_inference_graph.pb dosyasini alin" -ForegroundColor White
Write-Host "  4. Config dosyasini GitHub'dan indirin" -ForegroundColor White
Write-Host "  5. Dosyalari '$modelKlasoru' klasorune koyun" -ForegroundColor White

Write-Host ""
Write-Host "Coco.names dosyasini olusturuluyor..." -ForegroundColor Cyan
$cocoNames = @"
person
bicycle
car
motorcycle
airplane
bus
train
truck
boat
traffic light
fire hydrant
stop sign
parking meter
bench
bird
cat
dog
horse
sheep
cow
elephant
bear
zebra
giraffe
backpack
umbrella
handbag
tie
suitcase
frisbee
skis
snowboard
sports ball
kite
baseball bat
baseball glove
skateboard
surfboard
tennis racket
bottle
wine glass
cup
fork
knife
spoon
bowl
banana
apple
sandwich
orange
broccoli
carrot
hot dog
pizza
donut
cake
chair
couch
potted plant
bed
dining table
toilet
tv
laptop
mouse
remote
keyboard
cell phone
microwave
oven
toaster
sink
refrigerator
book
clock
vase
scissors
teddy bear
hair drier
toothbrush
"@

$cocoNamesPath = Join-Path $modelKlasoru "coco.names"
$cocoNames | Out-File -FilePath $cocoNamesPath -Encoding UTF8
Write-Host "  coco.names olusturuldu" -ForegroundColor Green

Write-Host ""
Write-Host "Islem tamamlandi!" -ForegroundColor Green
Write-Host ""
Write-Host "Indirilen dosyalar:" -ForegroundColor Cyan
Get-ChildItem -Path $modelKlasoru | ForEach-Object {
    $boyut = [math]::Round($_.Length / 1KB, 2)
    Write-Host "  - $($_.Name) ($boyut KB)" -ForegroundColor White
}

Write-Host ""
Write-Host "Projeyi calistirmak icin: dotnet run" -ForegroundColor Yellow



