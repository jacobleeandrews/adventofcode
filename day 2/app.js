const fs = require('fs')
const readline = require('readline');
var validPasswordsForSledRentalPlace = 0;
var validPasswordsForTobagganRentalPlace = 0;

const readInterface = readline.createInterface({
    input: fs.createReadStream('input.txt'),
    output: process.stdout,
    console: false
});

readInterface.on('line', (line) => {
    let occurrenceCount = 0;
    let characterToSearchFor = line.match(/[a-zA-Z]/).pop();
    let lineToSearchThrough = line.split(':')[1].trim();
    

    //determines the number of valid passwords for the sled rental place
    let lowerLimit = parseInt(line.split('-')[0]);
    let upperLimit = parseInt(line.split('-')[1].replace(/\D/g, ''));

    let occurrences = lineToSearchThrough.match(new RegExp(characterToSearchFor, "g"));
    if (!!occurrences) {
        occurrenceCount = occurrences.length;
        if (occurrenceCount <= upperLimit && occurrenceCount >= lowerLimit) validPasswordsForSledRentalPlace += 1;
    }

    //determines the number of valid passwords for the tobaggan rental place
    let firstPosition = lowerLimit - 1;
    let secondPosition = upperLimit - 1;

    if (lineToSearchThrough[firstPosition] === characterToSearchFor || lineToSearchThrough[secondPosition] === characterToSearchFor) {
        if (lineToSearchThrough[firstPosition] !== characterToSearchFor || lineToSearchThrough[secondPosition] !== characterToSearchFor) {
            validPasswordsForTobagganRentalPlace += 1;
        }
    }


});

readInterface.on('close', () => {
    console.log(`There are ${validPasswordsForSledRentalPlace} valid passwords for the sled rental place.`);
    console.log(`There are ${validPasswordsForTobagganRentalPlace} valid passwords for the tobaggan rental place.`);
});