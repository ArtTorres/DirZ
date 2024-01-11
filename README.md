# DirZ - Size directory evaluation tool 
[![Build](https://github.com/ArtTorres/DirZ/actions/workflows/build.yml/badge.svg)](https://github.com/ArtTorres/DirZ/actions/workflows/build.yml)

Shows the total size of the current directory or any specified path.

## Setup

1. Download on a location
2. Unzip
3. Execute

## Requirements
- .Net 6.0 or newer on Windows and Linux.

## Arguments
| Name | Alias | Description |
| -------- | ------- | ------- |
| --path | -p | Sets the path to be evaluated |
| --highlight | -hl | Color items based in their sizes |
| --order | -o | Order items based in their sizes, [ascendant / descendant] [asc / desc] |
| --show-hidden | -h | Show hidden files and directories. Disabled by default. |
| --verbose | -v | Displays a resume of evaluated files and encountered errors. |

### Examples
``` shell
# Windows Terminal

# Displays a list of directory sizes in current location
C:\> dirz.exe 

# Displays a list of directory sizes at a specified location
C:\> dirz.exe --path "D:\External"

# Displays a list of directory sizes in current location highlighting items by size color
C:\> dirz.exe --highlight

# Displays a list of directory sizes in current location ordering by descending size order
C:\> dirz.exe --order "ascendant"

# Displays a list of directory sizes in current location and shows a resume at the end
C:\> dirz.exe --verbose

# Displays a list of directory sizes (including hidden files and directories) at a specified location
C:\> dirz.exe --path "D:\External" --show-hidden

# Linux Terminal Supported

# Displays a list of directory sizes at a specified location
$ dirz --path "/home/External" --highlight --order "descendant"
```

### Example Output
``` shell
# Windows Terminal
C:\> dirz.exe --path "C:\DirZ"
  2.81 kB       .git
  8.99 kB       .github
200.26 kB       src
  7.09 kB       .gitignore
  1.07 kB       LICENSE
  1.32 kB       README.md
```

---
### Project References
- [Homepage](https://github.com/ArtTorres/DirZ)
- [License](https://github.com/ArtTorres/DirZ/blob/main/LICENSE)

### Issues or suggestions
- [Issues Section](https://github.com/ArtTorres/DirZ/issues)
