# Git Setup:
## Install Git  
Windows:
```
winget install Git.Git
```
Linux:
```
apt install git
```

## Init Local Git Repo
Open the Terminal in your IDE

Visual Studio
```
Ctrl + ` 
```

Rider:
```
Alt + F12
```

## initialize the git repo:

```
git init
```

### add gitignore

Windows:
```
New-Item .gitignore
```

Linux:
```
touch .gitignore
```
### commit changes

```
git add -A
```

```
git commit -m "message"
```
## Add to github
```
git remote add origin <your git repo url>
```

```
git push -u origin master
```

