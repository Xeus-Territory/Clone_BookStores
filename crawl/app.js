// link 1: https://tiki.vn//api/v2/products?limit=300&include=advertisement&aggregations=1&trackity_id=2be9e2c7-8ea9-2479-dbc1-f7fe50971941&category=316&page=1&src=c.8322.hamburger_menu_fly_out_banner&urlKey=sach-truyen-tieng-viet
// link 2: https://tiki.vn/api/v2/products?limit=48&include=advertisement&aggregations=1&trackity_id=2be9e2c7-8ea9-2479-dbc1-f7fe50971941&category=316&page=1&src=c.8322.hamburger_menu_fly_out_banner&urlKey=sach-truyen-tieng-viet
// link 3: https://tiki.vn/api/v2/products?limit=48&include=advertisement&aggregations=1&trackity_id=aed72fe2-4aa6-eb5a-543d-8b0448741dfc&category=8322&page=1&src=c.8322.hamburger_menu_fly_out_banner&urlKey=nha-sach-tiki



const axios = require('axios');
const fs = require('fs');
const path = require('path');

let numberOfItems = '300';
let page = '1';
let urlKey = 'sach-ba-me-em-be';
let category = '2527';

async function getUser() {
    try {//https://tiki.vn/api/v2/products?limit=48&include=advertisement&aggregations=1&trackity_id=b89b3e71-b22b-c37b-153a-d2442c5e52da&category=839&page=1&src=c.8322.hamburger_menu_fly_out_banner&urlKey=sach-van-hoc
      const response = await axios.get('https://tiki.vn/api/v2/products?limit=100&include=advertisement&aggregations=1&trackity_id=aed72fe2-4aa6-eb5a-543d-8b0448741dfc&category='+ category +'&page=1&src=c.8322.hamburger_menu_fly_out_banner&urlKey=' + urlKey);
      writeFile('/sampleData/sachbameembe.json', response.data);
    } catch (error) {
      console.error(error);
    }
}

(async () => {
    //await getKeyword();
    await getdatabook();
})();


function readFile(filePath)
{
    let rawdata = fs.readFileSync(path.join(process.cwd()+ filePath));
    return JSON.parse(rawdata);
}

function writeFile(filePath, data){
    let jData = JSON.stringify(data);
    fs.writeFileSync(path.join(process.cwd()+ filePath), jData);
}

// async function getKeyword() {
//     try {
//       const response = await axios.get('https://tiki.vn/api/v2/products?limit=48&include=advertisement&aggregations=1&trackity_id=b89b3e71-b22b-c37b-153a-d2442c5e52da&category=316&page=1&src=c.8322.hamburger_menu_fly_out_banner&urlKey=sach-truyen-tieng-viet');
//       let data = response.data.filters[0].values;
//       let keywords = {
//           data: []
//       };
//       data.forEach(e => {
//         keywords.data.push({
//             url_key: e.url_key,
//             category: e.query_value,
//             name: e.display_value,
//         });
//       })
//       writeFile('/sampleData/keywords.json', keywords);
//     } catch (error) {
//       console.error(error);
//     }
// }
async function getdatabook() {
  try{
    const response = await axios.get('https://tiki.vn/api/v2/products?limit=48&include=advertisement&aggregations=1&trackity_id=b89b3e71-b22b-c37b-153a-d2442c5e52da&category=316&page=1&src=c.8322.hamburger_menu_fly_out_banner&urlKey=sach-truyen-tieng-viet')
    let data = response.data.filters[0].values;
    data.forEach(e => {
      data.push({
        Id: e.id,
        Name: e.name,
        Prince: e.price,
        Images: e.thumbnail_url,
        Description: e.short_description,
      });
    })
    writeFile('/sampleData/data1.json', keywords);
  } catch (error) {
    console.error(error);
  }
}