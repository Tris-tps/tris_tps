# Tris - Gioco del Tris in C# Console con Intelligenza Artificiale

[![C#](https://img.shields.io/badge/C%23-FFF44F.svg?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![SQLite](https://img.shields.io/badge/SQLite-003B57.svg?style=for-the-badge&logo=sqlite&logoColor=white)](https://www.sqlite.org/index.html)
[![WebSocketSharp](https://img.shields.io/badge/WebSocketSharp-FF00FF.svg?style=for-the-badge)](https://github.com/sta/websocket-sharp)
[![.NET Core 7.0](https://img.shields.io/badge/.NET_Core-00CED1.svg?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/download/dotnet/3.1)
[![Colorful.Console](https://img.shields.io/badge/Colorful.Console-00AEFF.svg?style=for-the-badge)](https://github.com/tomakita/Colorful.Console)
[![Microsoft.EntityFrameworkCore](https://img.shields.io/badge/Microsoft.EntityFrameworkCore-722BE3.svg?style=for-the-badge)](https://docs.microsoft.com/en-us/ef/)
[![Microsoft.EntityFrameworkCore.Design](https://img.shields.io/badge/Microsoft.EntityFrameworkCore.Design-FF0000.svg?style=for-the-badge)](https://docs.microsoft.com/en-us/ef/)
[![Microsoft.EntityFrameworkCore.Sqlite](https://img.shields.io/badge/Microsoft.EntityFrameworkCore.Sqlite-0000FF.svg?style=for-the-badge)](https://docs.microsoft.com/en-us/ef/)
[![Microsoft.EntityFrameworkCore.Tools](https://img.shields.io/badge/Microsoft.EntityFrameworkCore.Tools-7D7F7D.svg?style=for-the-badge)](https://docs.microsoft.com/en-us/ef/)

Benvenuti nella repository del progetto "Tris", una versione del gioco del tris (Tic Tac Toe) implementato in C# Console. Questa implementazione consente ai giocatori di collegarsi tramite un server Sql Light per sfidarsi o giocare contro un avversario automatico basato sull'algoritmo Minimax. Il codice è rilasciato sotto la licenza [GNU General Public License v3.0](https://opensource.org/licenses/GPL-3.0).

## Collaboratori

I principali collaboratori per questo progetto sono:

- [Giorgio Citterio](https://github.com/GiorgioCitterio) (email: Giorgio.Citterio@issgreppi.it)
- [Umberto Colombo](https://github.com/umbecol) (email: Umberto.Colombo@issgreppi.it)
- [Andrea Panzeri](https://github.com/AndreaPanzeri) (email: Andrea.Panzeri@issgreppi.it)
- [Marco Passoni](https://github.com/MarcoPassoni) (email: Marco.Passoni@issgreppi.it)
- [Gabriele Viganò](https://github.com/GabrieleViga) (email: Gabriele.Vigano@issgreppi.it)

### Motivo del Progetto

Il progetto è stato ideato come parte integrante del percorso educativo della classe 5iB della scuola [Villa Greppi](https://istitutogreppi.edu.it/) di Monticello Brianza (LC). L'obiettivo principale è stato quello di applicare le conoscenze acquisite durante il corso di studi, mettendo in pratica le competenze informatiche nello sviluppo di un'applicazione funzionale e divertente.

## Descrizione Dettagliata del Programma

### Introduzione
Il progetto "Tris" è un'implementazione avanzata del classico gioco del tris (Tic Tac Toe) sviluppato in C# Console. Va oltre la semplice rappresentazione delle regole di base, offrendo un'esperienza interattiva in grado di connettere due giocatori attraverso un server Sql Light. Inoltre, presenta la possibilità di sfidare un avversario virtuale, il quale sfrutta un'intelligenza artificiale basata sull'algoritmo Minimax per creare partite competitive e coinvolgenti.

### Implementazione del Gioco
Questo progetto offre un'implementazione esaustiva e efficiente delle regole classiche del tris. Attraverso la console C#, consente a due giocatori di competere sullo stesso dispositivo o attraverso una connessione di rete. La struttura modulare e flessibile del codice consente l'interazione multi-client attraverso il server Sql Light, garantendo un'esperienza di gioco fluida e intuitiva.

### Giocatore AI e Algoritmo Minimax
Una delle caratteristiche più rilevanti di questo progetto è la presenza di un giocatore AI basato sull'algoritmo Minimax. Questa intelligenza artificiale apprende dalle mosse precedenti e calcola le mosse ottimali, valutando le possibili strategie di gioco per massimizzare le probabilità di vittoria o minimizzare le possibilità di sconfitta. L'algoritmo Minimax, implementato con cura e precisione, analizza in profondità le possibili mosse e crea un'esperienza di gioco sfidante, adattandosi alle scelte del giocatore umano.

### Ottimizzazione delle Prestazioni
Oltre all'implementazione di un'IA sofisticata, il progetto mira a ottimizzare le prestazioni attraverso un design attento e una gestione efficiente delle risorse. L'architettura del codice è stata sviluppata considerando l'ottimizzazione della memoria e l'efficienza algoritmica, garantendo un'esperienza di gioco fluida e reattiva.

### Espandibilità e Personalizzazione
La struttura modulare del codice permette una facile espandibilità e personalizzazione del gioco. I programmatori possono facilmente aggiungere nuove funzionalità, ottimizzare le strategie dell'IA o modificare l'interfaccia utente per adattarla alle proprie esigenze. L'architettura flessibile consente agli sviluppatori di estendere il progetto in molteplici direzioni, incoraggiando la creatività e l'innovazione nel mondo del gioco del tris.

### Sicurezza e Affidabilità
La sicurezza e l'affidabilità sono elementi chiave integrati nel progetto. Si pone particolare attenzione nella gestione delle connessioni e nel controllo delle operazioni di gioco, garantendo un'esperienza priva di problemi e sicura per tutti gli utenti.

### Roadmap e Sviluppo Futuro
Il progetto è in costante evoluzione, con una roadmap chiara e obiettivi definiti per migliorare ulteriormente l'esperienza di gioco. Le future implementazioni prevedono l'introduzione di nuove strategie per l'IA, miglioramenti nell'interfaccia utente e l'espansione delle funzionalità di gioco per offrire una varietà di modalità e sfide.

### Conclusioni
In conclusione, il progetto "Tris" offre non solo il gioco classico del tris ma anche una piattaforma di gioco avanzata che unisce divertimento, sfida e innovazione. L'implementazione dell'IA basata sull'algoritmo Minimax rende il gioco coinvolgente e stimolante, offrendo agli utenti un'esperienza appassionante e strategica.

## Come contribuire

Siamo aperti ai contributi dalla comunità. Se desideri contribuire a questo progetto, puoi farlo in vari modi:

- Segnalando bug o problemi tramite le issues.
- Aggiungendo nuove funzionalità o migliorando quelle esistenti con pull request.
- Migliorando la documentazione.
- Aiutando a testare il gioco e segnalando eventuali problemi.

## Installazione

Per iniziare a giocare al Tris con l'Intelligenza Artificiale, segui questi semplici passaggi:

1. Clona questo repository sul tuo computer.
2. Esegui il progetto utilizzando un ambiente che supporta C#.
3. Collega i client tramite il server Sql Light per iniziare a giocare o affrontare il bot AI.

## Ringraziamenti

Desideriamo ringraziare sinceramente tutti coloro che hanno contribuito o supportato questo progetto. Il vostro contributo è prezioso per noi!

## Licenza

Questo progetto è rilasciato sotto la licenza [GNU General Public License v3.0](https://opensource.org/licenses/GPL-3.0). Questa licenza ti dà la libertà di utilizzare, modificare e distribuire il codice sorgente in conformità con i termini specificati nella licenza.

È importante notare che, in virtù della licenza GNU GPL v3.0, qu

alsiasi modifica apportata a questo software e successivamente distribuita deve anch'essa essere rilasciata con la stessa licenza open source.

## Documentazione UML e Studio di Fattibilità

All'interno di questa repository, troverai una documentazione dettagliata che include diagrammi UML (Unified Modeling Language), tra cui diagrammi delle classi, diagrammi delle sequenze e diagrammi dei casi d'uso. Questa documentazione offre una visione chiara e strutturata della struttura del progetto, delle interazioni tra i componenti e delle funzionalità implementate.

Inoltre, è disponibile uno studio di fattibilità che analizza l'idoneità del progetto "Tris" e delle sue funzionalità. Questo studio fornisce una valutazione approfondita delle risorse necessarie, dei vincoli tecnici e delle potenziali sfide da affrontare durante lo sviluppo e la distribuzione del gioco.

La documentazione UML e lo studio di fattibilità sono strumenti cruciali per comprendere il progetto in modo approfondito e pianificare le fasi future del suo sviluppo.

---
