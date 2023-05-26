# BusHandlerWindowsService

NServiceBus Handler hosted in a windows service calling business MFC C++ dlls.

# Les objectifs

-	Démontrer qu’on peut se brancher sur une version de NServiceBus sous licence perpétuelle (version : 7.1.6). Note : on ne peut pas prendre les versions récentes sans payer en locatif désormais et suivant le nombre de endpoints.
-	Démontrer qu’on peut créer un service NT via .Net 7
-	Montrer que l’exécutable fourni n’a pas besoin d’installer quoi que ce soit sur la cible, j’ai choisi de produire un exécutable unique incluant toutes les dlls nécessaires dont celles de .Net 7 (Mode de publication avec les options : Produce single file + Self-contained)

![image](https://github.com/acoudene/BusHandlerWindowsService/assets/12967802/b27a58ea-f6d5-4b10-bdd6-432117a09a66)

- Montrer qu'on peut continuer à faire de l'interop vers du C++ MFC via .Net 7 et un wrapper C++/CLI

# Worflow visé

![Messaging Workflow - from command to business library  (1)](https://github.com/acoudene/BusHandlerWindowsService/assets/12967802/cc0ff13c-114a-4244-ab0d-b3db70e23297)

# Découpage

La solution du prototype est découpée en 7 projets :
-	1 projet (exécutable) qui émet une commande CreateOrder : **PublisherApp**
-	1 projet (exécutable) qui reçoit la commande CreateOrder et émet en ensuite un évènement OrderCreated : **SubscriberApp**
-	1 projet (exécutable ou Service Windows) qui reçoit l’évènement OrderCreated : **WindowsService**
-	1 projet de messages à échanger entre les différents exécutables : **Messages**
-	1 projet qui prend en charge tous les handles de récupération des messages : **BusHandler**
-	1 projet de librairie MFC C++ qui fait du pur métier : **MFCBusinessLibrary**
-	1 projet de wrapper d'interop C++/CLI entre la librairie MFC C++ et .Net : **WrapperBusinessLibrary**

![image](https://github.com/acoudene/BusHandlerWindowsService/assets/12967802/cb3d1df8-439d-47e7-828f-6fa3854898cf)

# Debug

Pour tester, rien de plus simple, faire simplement un lancement multiple :

![image](https://github.com/acoudene/BusHandlerWindowsService/assets/12967802/ac21e5fd-8e0a-4088-8651-0858ccd7b148)

# Déploiement

Pour installer le service windows sur votre machine, rien de plus simple faire dans un terminal :
-	WindowsService /Install
-	Puis faire Run dans la console services.msc

Pour désinstaller le service windows, faire :
-	WindowsService /Uninstall
-	Attention, j’ai pas corrigé mais il faut que le service soit démarré pour que la suppression se fasse correctement (je fais d’abord un équivalent de sc stop puis d’un sc delete)
