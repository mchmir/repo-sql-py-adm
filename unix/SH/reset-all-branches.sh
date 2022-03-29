#!/bin/bash



#  Setting some parameters -----------------------------------------------------

shopt -s expand_aliases
alias goto="cat >/dev/null <<"

BRANCH='release_4.1.34'



#  Function --------------------------------------------------------------------

resetonedir() {
  echo -e "\r\n\r\nResetting directory $1..."
  cd $1

  git reset --hard
  git clean -fd
  git clean -fdx
  git checkout $BRANCH

  CURRENTBRANCH=`git branch | grep "*" | sed 's/\* //'`
echo "CURRENTBRANCH=$CURRENTBRANCH, BRANCH=$BRANCH"
  if [ "$CURRENTBRANCH" != "$BRANCH" ] ; then
    git checkout master
  fi

  git reset --hard
  git clean -fd
  git clean -fdx
  git pull --rebase
#read -p ""

  if [ -d Doc ] ; then
    git submodule init Doc
    git submodule update Doc
    resetonedir Doc
  fi

  if [ -d LangData ] ; then
    git submodule init LangData
    git submodule update LangData
    resetonedir LangData
  fi

  cd ..
}

#  Prepare the local repository ------------------------------------------------

cd ~/git/Septim4.limited
for module in $( ls -d */ )
do
  modulename=`echo $module | sed 's/\///'`
  resetonedir $modulename
 #resetonedir $module
done



cd ~/git/Septim4.limited/Septim
VERSION=`cat info.xml | grep '<mrf module="SEPTIMAPP"' | sed -E 's/<mrf module="SEPTIMAPP" version="(.*)">/\1/'`
echo -e "New Septim bracnh is \e[92m$BRANCH\e[0m, version \e[92m$VERSION\e[0m."
cd ..
