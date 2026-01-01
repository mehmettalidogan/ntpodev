using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatBotLLM.Models;

namespace ChatBotLLM.Services
{
    public class OpenAIService : ILLMService
    {
        private Random random;
        private Dictionary<string, TopicData> topics;
        private string lastTopic;
        
        public string ModelName { get; private set; }
        public int MaxTokens { get; set; }
        
        public OpenAIService(string modelName = "gpt-3.5-turbo")
        {
            ModelName = modelName;
            MaxTokens = 1000;
            random = new Random();
            lastTopic = null;
            InitializeTopics();
        }
        
        private void InitializeTopics()
        {
            topics = new Dictionary<string, TopicData>();
            
            // Selamlaşma ve Temel İletişim
            topics["greeting"] = new TopicData
            {
                Keywords = new[] { "merhaba", "selam", "hey", "hello", "günaydın", "iyi günler", "hoş geldin" },
                Responses = new[] {
                    "Merhaba! Size nasıl yardımcı olabilirim? Programlama, teknoloji veya başka konularda sorularınız varsa çekinmeyin.",
                    "Selam! Bugün nasılsınız? Hangi konuda yardımcı olmamı istersiniz?",
                    "İyi günler! C#, OOP, veritabanları, algoritma gibi konularda size yardımcı olabilirim.",
                    "Hoş geldiniz! Programlama dünyasında merak ettiğiniz bir şey var mı?"
                },
                FollowUps = new[] {
                    "Size hangi konuda yardımcı olabilirim?",
                    "Ne öğrenmek istersiniz?"
                }
            };
            
            // Teşekkür
            topics["thanks"] = new TopicData
            {
                Keywords = new[] { "teşekkür", "sağol", "thank", "eyvallah", "saol", "çok teşekkür" },
                Responses = new[] {
                    "Rica ederim! Başka sorularınız varsa çekinmeyin.",
                    "Ne demek! Size yardımcı olabildiğime sevindim. Başka bir konuda da yardımcı olabilirim.",
                    "Memnuniyetle! Öğrenmek istediğiniz başka konular varsa sorabilirsiniz.",
                    "Her zaman! Başka merak ettiğiniz bir şey var mı?"
                }
            };
            
            // C# Programlama Dili
            topics["csharp"] = new TopicData
            {
                Keywords = new[] { "c#", "csharp", "c sharp", "dotnet", ".net" },
                Responses = new[] {
                    "C# Microsoft tarafından geliştirilen, güçlü ve modern bir programlama dilidir. Windows uygulamaları, web servisleri, oyunlar (Unity) ve daha fazlası için kullanılır. Hangi özelliği öğrenmek istersiniz?",
                    "C# hakkında ne öğrenmek istersiniz? OOP prensipleri, LINQ, async/await, delegates, generics gibi konularda yardımcı olabilirim.",
                    "C# ile .NET Framework kullanarak güçlü masaüstü ve web uygulamaları geliştirebilirsiniz. Syntax'ı temiz, öğrenmesi kolay ama çok yetenekli bir dildir.",
                    "C# şu an en popüler programlama dillerinden biri. Type-safe, object-oriented ve modern özelliklere sahip. Hangi versiyonunu kullanıyorsunuz?"
                },
                FollowUps = new[] {
                    "C# ile ilgili spesifik bir konu var mı merak ettiğiniz?",
                    "Hangi C# özelliğini detaylı öğrenmek istersiniz?"
                }
            };
            
            // OOP (Object-Oriented Programming)
            topics["oop"] = new TopicData
            {
                Keywords = new[] { "oop", "nesne", "object oriented", "sınıf", "class", "obje" },
                Responses = new[] {
                    "OOP (Nesneye Yönelik Programlama) 4 temel prensibe dayanır:\n\n1. **Encapsulation (Kapsülleme)**: Veriyi gizleme ve koruma\n2. **Inheritance (Kalıtım)**: Kod tekrarını azaltma\n3. **Polymorphism (Çok Biçimlilik)**: Aynı interface, farklı davranışlar\n4. **Abstraction (Soyutlama)**: Gereksiz detayları gizleme\n\nHangisi hakkında detay istersiniz?",
                    "Nesneye Yönelik Programlama, gerçek dünya problemlerini modellemeyi kolaylaştırır. Her şey class'lar ve object'ler üzerinden çalışır. Hangi prensibi derinlemesine öğrenmek istersiniz?",
                    "OOP modern yazılım geliştirmenin temelidir. Kodunuzu daha organize, bakımı kolay ve yeniden kullanılabilir yapar. Bir örnek görmek ister misiniz?",
                    "OOP prensipleri sayesinde büyük projeleri yönetilebilir parçalara ayırabilirsiniz. Interface'ler, abstract class'lar ve inheritance ile güçlü yapılar kurabilirsiniz."
                },
                FollowUps = new[] {
                    "Hangi OOP prensibini detaylı açıklayayım?",
                    "Bir kod örneği görmek ister misiniz?"
                }
            };
            
            // Encapsulation
            topics["encapsulation"] = new TopicData
            {
                Keywords = new[] { "encapsulation", "kapsülleme", "private", "public", "property", "field" },
                Responses = new[] {
                    "Encapsulation (Kapsülleme), class içindeki verileri private yapıp sadece public method'lar veya property'ler üzerinden erişim sağlamaktır. Böylece veri güvenliği ve kontrol sağlanır.\n\nÖrnek: private field'lar, public property'ler kullanmak.",
                    "Kapsülleme ile verilerinizi koruyabilirsiniz. Örneğin bir 'yaş' field'ını private yapıp, property ile sadece pozitif değerler atanmasını garantileyebilirsiniz. Bu data integrity sağlar.",
                    "Encapsulation'ın 3 ana faydası:\n1. Veri güvenliği\n2. Validation kontrolü\n3. Implementation gizleme\n\nGetter ve Setter method'ları bu prensibin temelidir."
                }
            };
            
            // Inheritance
            topics["inheritance"] = new TopicData
            {
                Keywords = new[] { "inheritance", "kalıtım", "extends", "base class", "derived", "miras" },
                Responses = new[] {
                    "Inheritance (Kalıtım), bir class'ın başka bir class'tan özellik ve method'ları miras almasıdır. C#'ta ':' operatörü ile kullanılır.\n\nÖrnek: class Dog : Animal\n\nBu sayede kod tekrarı azalır ve hiyerarşik yapılar kurabilirsiniz.",
                    "Kalıtım ile 'is-a' ilişkisi kurarsınız. Örneğin 'Dog is an Animal'. Base class'taki tüm public/protected member'lar derived class'ta kullanılabilir. Virtual method'larla polymorphism'i de destekler.",
                    "C#'ta single inheritance vardır (bir class sadece bir base class'tan türer) ama multiple interface implementation yapabilirsiniz. Bu hem esneklik hem güvenlik sağlar."
                }
            };
            
            // Polymorphism
            topics["polymorphism"] = new TopicData
            {
                Keywords = new[] { "polymorphism", "çok biçimlilik", "virtual", "override", "interface" },
                Responses = new[] {
                    "Polymorphism (Çok Biçimlilik) iki türlüdür:\n\n1. **Compile-time**: Method overloading\n2. **Runtime**: Method overriding (virtual/override)\n\nAynı method adı, farklı implementasyonlar. Bu esneklik ve genişletilebilirlik sağlar.",
                    "Polymorphism sayesinde bir base class referansı ile derived class'ların method'larını çağırabilirsiniz. Interface'ler de polymorphism'in güzel örnekleridir. Hangi türünü detaylandırayım?",
                    "Örnek: IShape interface'inden türeyen Circle, Square class'ları. Hepsi Draw() method'una sahip ama her biri farklı çizer. Bu polymorphism'dir!"
                }
            };
            
            // Abstraction
            topics["abstraction"] = new TopicData
            {
                Keywords = new[] { "abstraction", "soyutlama", "abstract class", "interface" },
                Responses = new[] {
                    "Abstraction (Soyutlama), gereksiz detayları gizleyip sadece önemli özellikleri göstermektir. Abstract class'lar ve interface'ler ile sağlanır.\n\nAbstract class: Hem concrete hem abstract method'lar içerebilir\nInterface: Sadece abstract member'lar (C# 8.0+ default implementation hariç)",
                    "Soyutlama ile 'ne yapıldığını' gösterip 'nasıl yapıldığını' gizlersiniz. Örneğin bir Database interface'i, altındaki SQL, MongoDB gibi detayları gizler. Kullanıcı sadece Save(), Get() gibi method'ları görür.",
                    "Abstract class vs Interface:\n- Abstract class: 'is-a' ilişkisi, constructor olabilir, field'lar olabilir\n- Interface: 'can-do' ilişkisi, sadece contract tanımlar, multiple implementation\n\nHangisini kullanmalısınız?"
                }
            };
            
            // Veritabanı
            topics["database"] = new TopicData
            {
                Keywords = new[] { "veritabanı", "database", "sql", "mysql", "mssql", "sqlite", "postgresql" },
                Responses = new[] {
                    "Veritabanları için C#'ta birkaç seçenek var:\n\n1. **ADO.NET**: Low-level, hızlı\n2. **Entity Framework**: ORM, kolay\n3. **Dapper**: Micro-ORM, performanslı\n\nHangisini kullanmak istersiniz?",
                    "SQL veritabanları (MSSQL, MySQL, PostgreSQL) ilişkisel veri için, NoSQL (MongoDB, Redis) ise esnek yapılar için idealdir. Projenizin ihtiyacı nedir?",
                    "Entity Framework ile code-first veya database-first yaklaşımı kullanabilirsiniz. LINQ ile sorgu yazabilir, migration'larla şema yönetebilirsiniz. Örnek görmek ister misiniz?"
                },
                FollowUps = new[] {
                    "Hangi veritabanı yönetim sistemini kullanıyorsunuz?",
                    "ORM mi yoksa raw SQL mi tercih edersiniz?"
                }
            };
            
            // Web Development
            topics["web"] = new TopicData
            {
                Keywords = new[] { "web", "asp.net", "mvc", "api", "rest", "http", "website" },
                Responses = new[] {
                    "C# ile web geliştirme için ASP.NET kullanılır:\n\n- **ASP.NET MVC**: Model-View-Controller pattern\n- **ASP.NET Web API**: RESTful servisler\n- **ASP.NET Core**: Modern, cross-platform\n- **Blazor**: WebAssembly ile C# frontend\n\nHangisi ilginizi çekiyor?",
                    "Web API geliştirmek için ASP.NET Core mükemmel. RESTful endpoint'ler oluşturup JSON döndürebilirsiniz. Dependency Injection, middleware, routing gibi modern özellikler built-in gelir.",
                    "MVC pattern ile web uygulamaları geliştirirken kod organizasyonu çok iyi olur. Model (veri), View (UI), Controller (logic) ayrımı net bir şekilde yapılır."
                }
            };
            
            // Algoritmalar
            topics["algorithm"] = new TopicData
            {
                Keywords = new[] { "algoritma", "algorithm", "sorting", "searching", "big o", "complexity" },
                Responses = new[] {
                    "Algoritmalar problem çözme adımlarıdır. Temel algoritma türleri:\n\n1. **Sıralama**: QuickSort, MergeSort, BubbleSort\n2. **Arama**: Binary Search, Linear Search\n3. **Graph**: BFS, DFS, Dijkstra\n4. **Dynamic Programming**: Memoization, Tabulation\n\nHangisini açıklayayım?",
                    "Big O notation algoritma karmaşıklığını ölçer:\n- O(1): Sabit\n- O(log n): Logaritmik\n- O(n): Lineer\n- O(n²): Quadratic\n\nEfficient kod yazmak için önemlidir!",
                    "Sorting algoritmaları:\n- QuickSort: Ortalama O(n log n), hızlı\n- MergeSort: Her zaman O(n log n), stable\n- HeapSort: O(n log n), in-place\n\nHangisini detaylandırayım?"
                }
            };
            
            // Veri Yapıları
            topics["datastructure"] = new TopicData
            {
                Keywords = new[] { "veri yapısı", "data structure", "list", "array", "stack", "queue", "tree", "graph", "hashmap" },
                Responses = new[] {
                    "Temel veri yapıları:\n\n1. **Array**: Sabit boyutlu, O(1) erişim\n2. **List**: Dinamik, esnek\n3. **Stack**: LIFO (Last In First Out)\n4. **Queue**: FIFO (First In First Out)\n5. **LinkedList**: Node tabanlı\n6. **Dictionary**: Key-Value pairs, O(1) lookup\n\nHangisini öğrenmek istersiniz?",
                    "C# Collections:\n- List<T>: En yaygın, dinamik array\n- Dictionary<K,V>: Hash table, hızlı lookup\n- HashSet<T>: Unique elemanlar\n- Queue<T> & Stack<T>: Özel kullanımlar\n\nProjenizde hangisini kullanmalısınız?",
                    "Advanced veri yapıları:\n- Binary Tree: Hiyerarşik veri\n- Graph: İlişkisel veri\n- Heap: Priority queue için\n- Trie: String arama için\n\nBunlar daha karmaşık problemler için idealdir."
                }
            };
            
            // Nasılsın / Hal hatır
            topics["howareyou"] = new TopicData
            {
                Keywords = new[] { "nasılsın", "nasıl gidiyor", "ne haber", "how are you", "naber" },
                Responses = new[] {
                    "Ben bir AI asistanıyım, her zaman hazırım! 😊 Siz nasılsınız? Size nasıl yardımcı olabilirim?",
                    "İyiyim, teşekkür ederim! Bugün hangi konuda yardımcı olabilirim? Programlama mı öğrenmek istiyorsunuz?",
                    "Harikayım! Size yardımcı olmak için buradayım. Hangi konuda sorularınız var?"
                }
            };
            
            // İsim sorma
            topics["name"] = new TopicData
            {
                Keywords = new[] { "adın ne", "ismin", "kim", "who are you", "sen kimsin" },
                Responses = new[] {
                    "Ben bir AI programlama asistanıyım. C#, OOP, algoritmalar ve yazılım geliştirme konularında size yardımcı olabilirim. Siz kendinizi tanıtmak ister misiniz?",
                    "Bana ChatBot diyebilirsiniz! Programlama öğrenmek ve sorularınızı yanıtlamak için buradayım. Sizinle tanışmak güzel!",
                    "AI tabanlı bir öğretim asistanıyım. Yazılım geliştirme yolculuğunuzda size rehberlik edebilirim."
                }
            };
            
            // Ne yapabilirsin / Yetenekler
            topics["capabilities"] = new TopicData
            {
                Keywords = new[] { "ne yapabilirsin", "neler yaparsın", "yardım", "help", "özelliklerin" },
                Responses = new[] {
                    "Size şu konularda yardımcı olabilirim:\n\n✅ C# programlama\n✅ OOP prensipleri\n✅ Veri yapıları ve algoritmalar\n✅ Web development (ASP.NET)\n✅ Veritabanı işlemleri\n✅ Design patterns\n✅ Debugging teknikleri\n\nHangi konuda başlamak istersiniz?",
                    "Programlama ile ilgili her konuda sorularınızı yanıtlayabilirim. Kod örnekleri verebilir, konseptleri açıklayabilir, best practice'leri paylaşabilirim. Ne öğrenmek istersiniz?",
                    "Benim uzmanlık alanlarım: C#, .NET, OOP, algoritma analizi, veritabanı tasarımı, yazılım mimarisi. Hangi konuda derinlemesine bilgi istersiniz?"
                }
            };
            
            // Design Patterns
            topics["patterns"] = new TopicData
            {
                Keywords = new[] { "design pattern", "singleton", "factory", "observer", "strategy", "desen", "pattern" },
                Responses = new[] {
                    "Design Patterns yazılım problemlerine kanıtlanmış çözümlerdir. Üç kategoriye ayrılır:\n\n1. **Creational**: Singleton, Factory, Builder\n2. **Structural**: Adapter, Decorator, Facade\n3. **Behavioral**: Observer, Strategy, Command\n\nHangisini detaylandırayım?",
                    "En popüler design patterns:\n- Singleton: Tek instance garantisi\n- Factory: Obje yaratma soyutlaması\n- Observer: Event-driven mimari\n- Strategy: Runtime'da davranış değiştirme\n\nÖrnek görmek ister misiniz?",
                    "Design patterns kullanmak kodunuzu daha maintainable, scalable ve anlaşılır yapar. SOLID prensiplerine de uygun yapılar kurmanıza yardımcı olur."
                }
            };
            
            // Genel konuşma - anlamadım
            topics["confused"] = new TopicData
            {
                Keywords = new[] { "anlamadım", "ne demek", "açıkla", "explain", "anlat" },
                Responses = new[] {
                    "Tabii, daha detaylı açıklayayım. Hangi kısmı anlamadınız? Örneklerle gösterebilirim.",
                    "Başka bir şekilde anlatayım. Daha basit bir örnekle başlayalım mı?",
                    "Anlıyorum, karmaşık gelmiş olabilir. Adım adım açıklayayım, hangi noktada takıldınız?"
                }
            };
            
            // Varsayılan cevaplar - Konuyla alakalı
            topics["default"] = new TopicData
            {
                Keywords = new string[] { },
                Responses = new[] {
                    "İlginç bir konu! Bununla ilgili daha fazla bilgi verebilir misiniz? Hangi açıdan yardımcı olmamı istersiniz?",
                    "Anlıyorum. Bu konuda size nasıl yardımcı olabilirim? Daha spesifik bir soru sorabilir misiniz?",
                    "Bu konuyla ilgili düşüncelerinizi duydum. Programlama veya teknoloji ile ilgili spesifik bir sorunuz var mı?",
                    "Evet, sizi dinliyorum. Hangi konuda detaylı bilgi istersiniz? C#, OOP, veritabanları, algoritmalar?"
                }
            };
        }
        
        public async Task<string> GenerateResponseAsync(string prompt, List<Message> context = null)
        {
            if (string.IsNullOrWhiteSpace(prompt))
                throw new ArgumentException("Prompt boş olamaz.");
            
            // Gerçekçi düşünme süresi simülasyonu
            await Task.Delay(random.Next(800, 2000));
            
            string lower = prompt.ToLower().Trim();
            
            // Soru mu kontrol et
            bool isQuestion = lower.Contains("?") || 
                              lower.StartsWith("ne") || 
                              lower.StartsWith("nasıl") || 
                              lower.StartsWith("neden") || 
                              lower.StartsWith("kim") ||
                              lower.StartsWith("nerede") ||
                              lower.StartsWith("hangi") ||
                              lower.Contains("what") ||
                              lower.Contains("how") ||
                              lower.Contains("why");
            
            // Context'ten son konuyu al
            string contextInfo = GetContextInfo(context);
            
            // En iyi eşleşen topic'i bul
            var matchedTopic = FindBestMatchingTopic(lower);
            
            if (matchedTopic != null)
            {
                lastTopic = matchedTopic;
                var response = GetResponse(matchedTopic, isQuestion);
                
                // Context'e göre ek bilgi ekle
                if (!string.IsNullOrEmpty(contextInfo) && matchedTopic == lastTopic)
                {
                    response += "\n\n" + topics[matchedTopic].FollowUps[random.Next(topics[matchedTopic].FollowUps.Length)];
                }
                
                return response;
            }
            
            // Hiçbir topic eşleşmediyse default cevap ver
            return GetResponse("default", isQuestion);
        }
        
        private string FindBestMatchingTopic(string message)
        {
            int maxMatchCount = 0;
            string bestMatch = null;
            
            foreach (var topic in topics)
            {
                if (topic.Key == "default") continue;
                
                int matchCount = topic.Value.Keywords.Count(keyword => message.Contains(keyword));
                
                if (matchCount > maxMatchCount)
                {
                    maxMatchCount = matchCount;
                    bestMatch = topic.Key;
                }
            }
            
            // En az bir keyword eşleşmesi olmalı
            return maxMatchCount > 0 ? bestMatch : null;
        }
        
        private string GetResponse(string topicKey, bool isQuestion)
        {
            if (topics.ContainsKey(topicKey))
            {
                var responses = topics[topicKey].Responses;
                return responses[random.Next(responses.Length)];
            }
            return topics["default"].Responses[0];
        }
        
        private string GetContextInfo(List<Message> context)
        {
            if (context == null || context.Count < 2)
                return null;
            
            // Son birkaç mesajı analiz et
            var recentMessages = context.Skip(Math.Max(0, context.Count - 3)).ToList();
            var messageText = string.Join(" ", recentMessages.Select(m => m.Content.ToLower()));
            
            return messageText;
        }
        
        // Topic verisi için yardımcı sınıf
        private class TopicData
        {
            public string[] Keywords { get; set; }
            public string[] Responses { get; set; }
            public string[] FollowUps { get; set; } = new string[] { };
        }
    }
}

