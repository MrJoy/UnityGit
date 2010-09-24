# OVERVIEW

UnityGit is a very young project aiming to provide some degree of integration 
between [Git](http://git-scm.com) and [Unity](http://unity3d.com).

At present, it's likely it will only work on MacOS X, and even then full 
functionality requires some manual effort.

In short, you probably don't want to be using this yet.


# INSTALLATION

Clone this repo somewhere under Assets/Editor in your project.  If your project
is not yet checked into git, then you'll need to do the appropriate setup and 
add this as a submodule (google: git-submodule).

If you already use git for your project, then just add this as a submodule.


# USAGE

* Click "Tools" -> "Git"
* Click on one of the options, as needed.


# COMPATIBILITY

* Unity 2.6.x: Untested.
* Unity/iPhone 1.x: Untested.
* Unity 3.0.0: Works.


# LICENSE

Dual licensed under the terms of the MIT X11 or GNU GPL, as per the original code.


# TODO

* Provide options for setting up a project under git.
* Configurable path to git/gitk binaries.
** Option to substitute gitx for gitk/git-gui.
* Don't require a manual symlink in /usr/bin for gitk.
* Windows support.
* Actual GUI interface for various git operations.