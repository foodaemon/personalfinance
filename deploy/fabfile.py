import os, sys, shutil, subprocess

PROJECT_ROOT = os.path.abspath(os.path.join(os.path.dirname(__file__), '../'))
PROJECT_BUILD_PATH = os.path.join(PROJECT_ROOT, 'Web')

PROJECT_DIRS = ['Content', 'Scripts', 'bin', 'Views']
PROJECT_FILES = ['Global.asax', 'Web.config']
PUBLISH_DIR = '/opt/www/finance'

def restore_nuget_packages():
    pid = subprocess.Popen(["nuget", "restore", "Finance.sln"], stdout=subprocess.PIPE)
    output, error = pid.communicate()
    # print(p.returncode)
    return error

def build_project():
    pid = subprocess.Popen(['xbuild','/p:BuildWithMono="true"', '/p:Configuration=Release', 'Finance.sln'], stdout=subprocess.PIPE)
    output, error = pid.communicate()
    return error

def deploy_build():
    if os.path.exists(PUBLISH_DIR):
        for directory in PROJECT_DIRS:
            subprocess.Popen(['cp', '-R', os.path.join(PROJECT_BUILD_PATH, directory), PUBLISH_DIR], stdout=subprocess.PIPE)
        for f in PROJECT_FILES:
            subprocess.Popen(['cp', '-f', os.path.join(PROJECT_BUILD_PATH, f), PUBLISH_DIR], stdout=subprocess.PIPE)

def main():
    if restore_nuget_packages() is None:
        build_project()
    else:
        sys.exit()
    
    # if above went successful build the project
    if build_project() is None:
        deploy_build()
    else:
        sys.exit()

if(__name__ == "__main__"):
    main()
