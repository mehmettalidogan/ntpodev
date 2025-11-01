const express = require('express');
const http = require('http');
const socketIo = require('socket.io');
const path = require('path');

const app = express();
const server = http.createServer(app);
const io = socketIo(server, {
    cors: {
        origin: "*",
        methods: ["GET", "POST"]
    }
});

const PORT = process.env.PORT || 3000;
const connectedUsers = new Map();

// Ana sayfa
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'public', 'index.html'));
});

// Socket.IO bağlantı yönetimi
io.on('connection', (socket) => {
    console.log(`Yeni kullanıcı bağlandı: ${socket.id}`);

    // Kullanıcı katılımı
    socket.on('join', (username) => {
        console.log(`Kullanıcı katıldı: ${username}`);
        
        // Kullanıcıyı kaydet
        connectedUsers.set(socket.id, username);
        socket.username = username;

        // Tüm kullanıcılara katılım mesajı gönder
        socket.broadcast.emit('message', {
            username: 'Sistem',
            message: `${username} sohbete katıldı`,
            timestamp: new Date().toLocaleTimeString('tr-TR')
        });

        // Kullanıcı listesini güncelle
        updateUserList();
    });

    // Mesaj alma
    socket.on('message', (data) => {
        console.log(`Mesaj alındı: ${data.username}: ${data.message}`);
        
        // Tüm kullanıcılara mesajı gönder
        io.emit('message', {
            username: data.username,
            message: data.message,
            timestamp: data.timestamp || new Date().toLocaleTimeString('tr-TR')
        });
    });

    // Kullanıcı ayrılışı
    socket.on('leave', (username) => {
        console.log(`Kullanıcı ayrıldı: ${username}`);
        
        // Kullanıcıyı listeden çıkar
        connectedUsers.delete(socket.id);

        // Diğer kullanıcılara ayrılış mesajı gönder
        socket.broadcast.emit('message', {
            username: 'Sistem',
            message: `${username} sohbetten ayrıldı`,
            timestamp: new Date().toLocaleTimeString('tr-TR')
        });

        // Kullanıcı listesini güncelle
        updateUserList();
    });

    // Bağlantı kesilme
    socket.on('disconnect', () => {
        const username = connectedUsers.get(socket.id);
        if (username) {
            console.log(`Kullanıcı bağlantısı kesildi: ${username}`);
            
            connectedUsers.delete(socket.id);
            
            // Diğer kullanıcılara bağlantı kesilme mesajı gönder
            socket.broadcast.emit('message', {
                username: 'Sistem',
                message: `${username} bağlantısı kesildi`,
                timestamp: new Date().toLocaleTimeString('tr-TR')
            });

            // Kullanıcı listesini güncelle
            updateUserList();
        }
    });
});

// Kullanıcı listesini güncelleme fonksiyonu
function updateUserList() {
    const users = Array.from(connectedUsers.values());
    io.emit('userList', users);
}

// Sunucuyu başlat
server.listen(PORT, () => {
    console.log(`Sunucu http://localhost:${PORT} adresinde çalışıyor`);
});

// Graceful shutdown
process.on('SIGINT', () => {
    console.log('\nSunucu kapatılıyor...');
    server.close(() => {
        console.log('Sunucu kapatıldı.');
        process.exit(0);
    });
});
