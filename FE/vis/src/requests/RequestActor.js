const url = require('../config/config');

const domain = url.url;

const getSpecificActor = async (id) => {
    console.log("jsem tady");
    try {
        const response = await fetch(domain + 'Actor/' + id);
        const data = await response.json();
        return data;
    } 
    catch (error) {
        console.error('Error fetching data:', error);
    }
}

module.exports = {
    getSpecificActor,
}