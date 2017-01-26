# DOTA
Defense Of The mAgi


# Git convension

Unity with Git:
 - Because there exists some merge issues with prefabs and scenes, we need to set up proper merge tools for unity
 - Official documentation: https://docs.unity3d.com/Manual/SmartMerge.html
 - Additional guide: https://gist.github.com/Ikalou/197c414d62f45a1193fd

Git Flow: http://nvie.com/posts/a-successful-git-branching-model/
 - Basically, core development should be done on "feature/(.*+)" branch, go through code review and the be merged to "develop" branch
 - "master" branch is only used to track official releases. (prob. every assignment deadlines)

Linear history: http://devblog.nestoria.com/post/98892582763/maintaining-a-consistent-linear-history-for-git
 - When merging, "develop" branch should be the first parant of merge commit (to guarentee linear history.)
