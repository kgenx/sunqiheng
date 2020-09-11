#Virgil Sync

Virgil Sync encrypts user data using Virgil Crypto library, Virgil Public and Private keys infrastructure APIs and Dropbox Rest API.

Virgil Sync gets the list of files from application folder on Dropbox and synchronizes them with the files from mapped folder on the user’s machine. Files from the user’s selected folder are encrypted with the user’s public key and file names are hashed with SHA256. 

The general scheme:
![Virgil Sync Process](https://github.com/VirgilSecurity/virgil-sync/blob/master/Setup/virgil-sync-scheme.png "Virgil Sync Process")

1.	User creates Virgil account using Virgil CLI software.
2.	User choses local folder on his machine which will be used as a place where to store decrypted data.
3.	Virgil Sync starts to listen for the local file system changes.
4.	Every time local file system notifies Virgil sync process about any change, Virgil Sync software updates encrypted files in the user's Dropbox account using user's public key.
5.	Every time Dropbox file system changes (i.e. via Dropbox web interfaces or via another active machine of current user).
6.	Changed files are downloaded and decrypted using user's private key.


CLI version exposes 3 commands: 

```
 