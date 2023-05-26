# BusHandlerWindowsService

NServiceBus Handler hosted in a windows service

Les objectifs :
-	Démontrer qu’on peut se brancher sur une version de NServiceBus sous licence perpétuelle (version : 7.1.6). Note : on ne peut pas prendre les versions récentes sans payer en locatif désormais et suivant le nombre de endpoints.
-	Démontrer qu’on peut créer un service NT via .Net 7
-	Montrer que l’exécutable fourni n’a pas besoin d’installer quoi que ce soit sur la cible, j’ai choisi de produire un exécutable unique incluant toutes les dlls nécessaires dont celles de .Net 7 (Mode de publication avec les options : Produce single file + Self-contained)

![image](https://github.com/acoudene/BusHandlerWindowsService/assets/12967802/a309e18f-4733-4e5b-a131-a298bdcdccf9)

La solution du prototype est découpée en 5 projets :
-	1 projet (exécutable) qui émet une commande CreateOrder : PublisherApp
-	1 projet (exécutable) qui reçoit la commande CreateOrder et émet en ensuite un évènement OrderCreated : SubscriberApp
-	1 projet (exécutable ou Service Windows) qui reçoit l’évènement OrderCreated : WindowsService
-	1 projet de messages à échanger entre les différents exécutables : Messages
-	1 projet qui prend en charge tous les handles de récupération des messages : BusHandler

![image](https://github.com/acoudene/BusHandlerWindowsService/assets/12967802/350c4a74-ef60-4dd6-beae-40f6df023d1f)

Pour tester, rien de plus simple, faire simplement un lancement multiple :

![image](https://github.com/acoudene/BusHandlerWindowsService/assets/12967802/ac21e5fd-8e0a-4088-8651-0858ccd7b148)

Pour installer le service windows sur votre machine, rien de plus simple faire dans un terminal :
-	WindowsService /Install
-	Puis faire Run dans la console services.msc

Pour désinstaller le service windows, faire :
-	WindowsService /Uninstall
-	Attention, j’ai pas corrigé mais il faut que le service soit démarré pour que la suppression se fasse correctement (je fais d’abord un équivalent de sc stop puis d’un sc delete)

La prochaine étape : 
-	inclure du C++ par interop ou COM utilisé par le handler par exemple…
