# DOTA
Defense Of The mAgi


# Git convension
Git Flow: http://nvie.com/posts/a-successful-git-branching-model/
 - Basically, core development should be done on "feature/(.*+)" branch, go through code review and the be merged to "develop" branch
 - "master" branch is only used to track official releases. (prob. every assignment deadlines)

Linear history: http://devblog.nestoria.com/post/98892582763/maintaining-a-consistent-linear-history-for-git
 - When merging, "develop" branch should be the first parant of merge commit (to guarentee linear history.)
