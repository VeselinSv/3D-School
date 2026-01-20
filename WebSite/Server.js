const express = require('express');
const cors = require('cors');
const path = require('path');
const app = express();
require('dotenv').config();
const PORT = process.env.PORT || 3001;

// Middleware
app.use(cors());
app.use(express.json());
app.use(express.static('public'));

// Root route - зарежда Index.html
app.get('/', (req, res) => {
  res.sendFile(path.join(__dirname, 'public', 'Index.html'));
});

// Mock данни за училището
const schoolData = {
  info: {
    name: 'СУ "ЙОРДАН ЙОВКОВ"',
    city: 'СЛИВЕН',
    address: 'гр. Сливен',
    phone: '+359 44 XXX XXX',
    email: 'school@10sou.sliven.net'
  },
  
  news: [
    {
      id: 1,
      title: 'КОЛЕДЕН БАЗАР В СУ „ЙОРДАН ЙОВКОВ" – СЛИВЕН',
      date: '23.12.2025',
      excerpt: 'С много усмивки, празнично настроение и коледен дух на 22.12.2025 г. в СУ „Йордан Йовков" – Сливен се проведе Коледен базар...',
      image: 'https://via.placeholder.com/400x300/2196F3/ffffff?text=Коледен+Базар',
      category: 'events'
    },
    {
      id: 2,
      title: 'УЧЕНИЦИТЕ ПОЛУЧИХА ОТГОВОРИ ОТ ДЯДО КОЛЕДА',
      date: '22.12.2025',
      excerpt: 'В навечерието на празниците учениците от 2. А клас получиха вълнуваща изненада – лични отговори на писмата си до Дядо Коледа...',
      image: 'https://via.placeholder.com/400x300/1976D2/ffffff?text=Дядо+Коледа',
      category: 'activities'
    },
    {
      id: 3,
      title: 'СУРВА, ВЕСЕЛА ГОДИНА!',
      date: '21.12.2025',
      excerpt: 'С цел да съхранят традицията, учениците от 3. А клас, заедно със своите родители, се включиха с много вдъхновение в проекта...',
      image: 'https://via.placeholder.com/400x300/0D47A1/ffffff?text=Сурва',
      category: 'traditions'
    },
    {
      id: 4,
      title: 'КОЛЕДНО ВЪЛШЕБСТВО В ПЪРВИ КЛАС',
      date: '19.12.2025',
      excerpt: 'С много вълнение и блестящи очи първокласниците посрещнаха Коледа по най-красивия начин – с празнична коледна работилничка...',
      image: 'https://via.placeholder.com/400x300/42A5F5/ffffff?text=Първи+клас',
      category: 'activities'
    }
  ],
  
  sections: [
    {
      id: 'about',
      title: 'За Училището',
      icon: '🏫',
      description: 'СУ "Йордан Йовков" е модерно образователно заведение с богата история и традиции.'
    },
    {
      id: 'admission',
      title: 'Прием',
      icon: '📝',
      description: 'Информация за прием в 1-ви, 5-ти и 8-ми клас.'
    },
    {
      id: 'activities',
      title: 'Дейности',
      icon: '🎨',
      description: 'Извънкласни дейности, клубове, спорт и изкуства.'
    },
    {
      id: 'team',
      title: 'Екип',
      icon: '👥',
      description: 'Нашият професионален преподавателски екип.'
    },
    {
      id: 'contacts',
      title: 'Контакти',
      icon: '📞',
      description: 'Свържете се с нас за повече информация.'
    }
  ],
  
  stats: [
    { label: 'Ученици', value: '500+', icon: '🎓' },
    { label: 'Учители', value: '45+', icon: '👨‍🏫' },
    { label: 'Класни стаи', value: '24', icon: '🚪' },
    { label: 'Години традиции', value: '40+', icon: '📚' }
  ]
};

// Училищна база знания за AI асистента
const schoolKnowledge = `
ТИ СИ AI АСИСТЕНТ НА СУ "ЙОРДАН ЙОВКОВ" - СЛИВЕН

ОСНОВНА ИНФОРМАЦИЯ:
- Име: Средно училище "Йордан Йовков"
- Град: Сливен, България
- Години история: 40+ години традиции
- Брой ученици: 500+
- Брой учители: 45+
- Класни стаи: 24

ПРИЕМ НА УЧЕНИЦИ:

ПЪРВИ КЛАС:
- Възраст: Деца навършили 7 години
- Документи: Акт за раждане, медицинско свидетелство, снимки
- Срок: Обичайно април-май
- Изисквания: Няма входни изпити

ПЕТИ КЛАС (след завършен 4-ти клас):
- Документи: Свидетелство за завършен начален етап, характеристика
- Изисквания: Успех от предходното образование
- Профили: Общообразователни класове

ОСМИ КЛАС (след завършен 7-ми клас):
- Документи: Удостоверение за завършен клас, оценки
- Бални системи: Съгласно МОН
- Профили: Хуманитарни и природо-математически профили

ИЗВЪНКЛАСНИ ДЕЙНОСТИ:
- Спортни клубове (футбол, волейбол, баскетбол)
- Изкуства (музика, танци, рисуване)
- Олимпиади и състезания
- Екскурзии и образователни пътувания
- Проекти и работилници

УЧЕБНА БАЗА:
- 24 класни стаи
- Компютърни кабинети
- Спортна зала
- Библиотека
- Столова

ЦЕЛОДНЕВНА ОРГАНИЗАЦИЯ:
- Целодневно обучение за началните класове
- Занимални по интереси
- Подкрепа при уроците
- Ресурсно подпомагане

КОНТАКТИ:
- Адрес: гр. Сливен
- Телефон: +359 44 XXX XXX
- Email: school@10sou.sliven.net
- Работно време: 8:00 - 17:00 (учебни дни)

ВАЖНО: Винаги отговаряй на български език. Бъди приятелски, информативен и точен. 
Ако не знаеш точен отговор, кажи че може да се свържат директно с училището.
`;

// API Routes
app.get('/api/school-info', (req, res) => {
  res.json(schoolData.info);
});

app.get('/api/news', (req, res) => {
  const { category, limit } = req.query;
  let news = schoolData.news;
  
  if (category) {
    news = news.filter(item => item.category === category);
  }
  
  if (limit) {
    news = news.slice(0, parseInt(limit));
  }
  
  res.json(news);
});

app.get('/api/news/:id', (req, res) => {
  const newsItem = schoolData.news.find(item => item.id === parseInt(req.params.id));
  if (newsItem) {
    res.json(newsItem);
  } else {
    res.status(404).json({ error: 'Новината не е намерена' });
  }
});

app.get('/api/sections', (req, res) => {
  res.json(schoolData.sections);
});

app.get('/api/stats', (req, res) => {
  res.json(schoolData.stats);
});

// Контактна форма
app.post('/api/contact', (req, res) => {
  const { name, email, message } = req.body;
  
  if (!name || !email || !message) {
    return res.status(400).json({ 
      success: false, 
      error: 'Всички полета са задължителни' 
    });
  }
  
  console.log('Получено съобщение:', { name, email, message });
  
  res.json({ 
    success: true, 
    message: 'Вашето съобщение беше изпратено успешно!' 
  });
});

// AI Chat Endpoint
app.post('/api/chat', async (req, res) => {
  try {
    const { message, conversationHistory = [] } = req.body;

    if (!message) {
      return res.status(400).json({ 
        success: false, 
        error: 'Моля, въведете съобщение' 
      });
    }

    // Подготовка на съобщенията за Claude API
    const messages = [
      ...conversationHistory.map(msg => ({
        role: msg.role,
        content: msg.content
      })),
      {
        role: 'user',
        content: message
      }
    ];

    // Извикване на Claude API
    const response = await fetch('https://api.anthropic.com/v1/messages', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'anthropic-version': '2023-06-01'
      },
      body: JSON.stringify({
        model: 'claude-sonnet-4-20250514',
        max_tokens: 1024,
        system: schoolKnowledge,
        messages: messages
      })
    });

    if (!response.ok) {
      throw new Error(`Claude API error: ${response.status}`);
    }

    const data = await response.json();
    
    // Извличане на отговора
    const aiResponse = data.content
      .filter(block => block.type === 'text')
      .map(block => block.text)
      .join('\n');

    res.json({
      success: true,
      response: aiResponse,
      conversationHistory: [
        ...conversationHistory,
        { role: 'user', content: message },
        { role: 'assistant', content: aiResponse }
      ]
    });

  } catch (error) {
    console.error('Chat error:', error);
    res.status(500).json({
      success: false,
      error: 'Грешка при обработка на съобщението. Моля, опитайте отново.'
    });
  }
});

app.listen(PORT, () => {
  console.log(`🚀 Server is running on http://localhost:${PORT}`);
  console.log(`📡 API endpoints:`);
  console.log(`   GET  /api/school-info`);
  console.log(`   GET  /api/news`);
  console.log(`   GET  /api/news/:id`);
  console.log(`   GET  /api/sections`);
  console.log(`   GET  /api/stats`);
  console.log(`   POST /api/contact`);
  console.log(`   POST /api/chat (AI Assistant)`);
});
