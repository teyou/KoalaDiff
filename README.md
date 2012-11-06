KoalaDiff
=========

Image diff viewer application which can integrate with version control client software (i.e. TortoiseSVN)

**Screenshot**

**Command-Line Support**
    KoalaDiff [-?]  [-f] [-a]  [-s] [-o] [-v]  [-m] [-n]  [-h *highlightvalue*] [-l *leftdesc*] [-r *rightdesc*] *leftpath* *rightpath*
    
Entering the command with no parameters or pathnames simply opens the KoalaDiff window. Parameters are prefixed with dash ( - ) character. Pathnames have no prefix character.

* -? opens KoalaDiff command-line quick start manual on the popup window.
* -f fits the image to the window. Only one parameter between –f and –a can be specified at the same time.
* -a shows the image in the actual size. Only one parameter between –f and –a can be specified at the same time.
* -s shows the image within side-by-side layout mode. Only one parameter between -s,-o and –v can be specified at the same time.
* -o shows the image within overlay layout mode. Only one parameter between -s,-o and –v can be specified at the same time.
* -v shows the image within overlay flicker layout mode. Only one parameter between -s,-o and –v can be specified at the same time.
* -m maximize the KoalaDiff window.
* -n shows the KoalaDiff window in the normal size.
* -h enables the highlight and highlight the image based on the given value.
* -l specifies a description in the left side title bar, overriding the default filename text.
* -r specifies a description in the right side title bar, just like –l.
* *leftpath* specifies the file to open on the left side.
* *rightpath* specifies the file to open on the right side.
