const fs = require('fs');
const path = require('path');
const readline = require('readline');
const ignore = ['node_modules', '.vs', '.git', 'output', 'bin', 'obj'];

const rl = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

rl.question('Wie heißt das Projekt aktuell? (Empty = Contract.Architecture) ', (oldName) => {
    if (!oldName || oldName.trim().length === 0) {
        oldName = 'Contract.Architecture';
    }

    rl.question('Wie soll das Projekt heißen? ', (newName) => {
        console.log('Start renaming...');
        renameAll(process.cwd(), oldName, newName);
        console.log('Finished renaming.');

        rl.close();
    });
});


function renameAll(rootPath, oldName, newName, thisPath = '') {
    const elements = fs.readdirSync(path.join(rootPath, thisPath));
    for (const elementName of elements) {
        let subPath = path.join(thisPath, elementName);
        if (fs.statSync(subPath).isDirectory()) {
            if (elementName.includes(oldName)) {
                const oldPath = subPath;
                const newPath = subPath.split(oldName).join(newName);
                rename(rootPath, oldPath, newPath);
                subPath = newPath;
            }
            if (!ignore.includes(elementName)) {
                renameAll(rootPath, oldName, newName, subPath);
            }
        } else {
            if (elementName.includes(oldName)) {
                const oldPath = subPath;
                const newPath = subPath.split(oldName).join(newName);
                rename(rootPath, oldPath, newPath);
            }
        }
    }
}

function rename(rootPath, oldPath, newPath) {
    console.log('Renaming: ' + oldPath + ' -> ' + newPath);

    const oldPathFull = path.join(rootPath, oldPath);
    const newPathFull = path.join(rootPath, newPath);

    fs.renameSync(oldPathFull, newPathFull);
}