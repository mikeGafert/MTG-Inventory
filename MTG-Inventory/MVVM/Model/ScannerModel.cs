using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTG_Inventory.Core;
using MTG_Inventory.Classes;
using MTG_Inventory.MVVM.ViewModel;

namespace MTG_Inventory.MVVM.Model
{
    internal class ScannerModel
    {
        //detect(on_detect);

    }
}
/// <summary>
/// Original Python Code
/// 
/// Bekommt den Snapshot von der Kamera und fragt diesen im Datenbestand aller Bilder (Hash) nach.
/// 
/// Er er einen Treffer hat, sucht er sich die restlichen Daten aus dem Datenbestand aller Karten 
/// raus und schreibt die Karte ins persoenliche Inventory.
/// 
/// db war eine "simple file-based database, optimized to append and return last n items."
/// </summary>
//def on_detect(card_image):
//    image = np.array(card_image).tolist()
//    resp = requests.post('http://localhost:8888',
//                         json ={ 'image': image, 'max_distance': 10000})
//    results = resp.json()['results']
//    if results:
//        result = results[0]
//        print('{} ({})'.format(result['card']['name'], result['id']))
//        db.append(result['id'])