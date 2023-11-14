# DirZ - Directory size evaluation tool [![DirZ](https://github.com/ArtTorres/DirZ/actions/workflows/dotnet-console.yml/badge.svg?branch=main)](https://github.com/ArtTorres/DirZ/actions/workflows/dotnet-console.yml)

Simple tool to evaluate the size of directories and files.

## Setup

1. Download on a location
2. Unzip
3. Execute

## Support
- .Net 7.0 - Any OS

## Parameters
| Parameter | Alias | Description |
| -------- | ------- | ------- |
| path | p | Sets the path to be evaluated |
| highlight | h | Color items based in their sizes |
| order | o | Order items based in their sizes, ["az" ascendant / "za" descendant] |

### Examples
``` shell
# Windows CMD

# Displays a list of directory sizes in current location
C:\> dirz.exe 

# Displays a list of directory sizes at a specified location
C:\> dirz.exe /path "D:\External>"

# Displays a list of directory sizes in current location highlighting items by size color
C:\> dirz.exe /highlight

# Displays a list of directory sizes in current location ordering by descending size order
C:\> dirz.exe /order "za"
```

> [!NOTE]
> Linux OS uses "-" dash symbol in their parameters.

### Examples
``` shell
# Ubuntu Terminal

# Displays a list of directory sizes at a specified location
$ dirz -path "/home/External>" -highlight -order "za"
```

### Output
``` shell
# Windows CMD
C:\>dirz.exe /path "C:\DirZ"
  2.81 kB       .git
  8.99 kB       .github
200.26 kB       src
  7.09 kB       .gitignore
  1.07 kB       LICENSE
  1.32 kB       README.md
```

---
## Project References
- [Homepage](https://github.com/ArtTorres/DirZ)
- [License](https://github.com/ArtTorres/DirZ/blob/main/LICENSE)
