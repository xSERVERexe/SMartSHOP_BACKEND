namespace SMartSHOP.Api.Data.Seed;

public record ProductSeed(string Name, string Description, decimal Price, string Category);

public static class CatalogSeedData
{
    public static readonly string[] Categories =
    [
        "Smartfony",
        "Tablety",
        "Laptopy",
        "Audio",
        "Smartwatche",
        "Monitory",
        "Akcesoria",
        "Dom i AGD"
    ];

    public static readonly ProductSeed[] Products =
    [
        // Smartfony
        new("Samsung Galaxy S24 Ultra 512GB", "Flagowy smartfon Samsung z rysikiem S Pen, tytanową ramką i ekranem Dynamic AMOLED 2X.", 5699.00m, "Smartfony"),
        new("Samsung Galaxy S24+ 256GB", "Smartfon Samsung z dużym ekranem QHD+, procesorem Exynos/Snapdragon i szybkim ładowaniem.", 4299.00m, "Smartfony"),
        new("Samsung Galaxy S24 128GB", "Kompaktowy smartfon Samsung z ekranem 120 Hz, aparatem 50 MP i funkcjami Galaxy AI.", 3499.00m, "Smartfony"),
        new("Samsung Galaxy S23 FE 128GB", "Smartfon Samsung z ekranem AMOLED 120 Hz, teleobiektywem i solidną wydajnością.", 2499.00m, "Smartfony"),
        new("Samsung Galaxy Z Fold6 256GB", "Składany smartfon Samsung z dużym ekranem wewnętrznym do wielozadaniowości i pracy.", 7499.00m, "Smartfony"),
        new("Samsung Galaxy Z Flip6 256GB", "Składany smartfon Samsung z praktycznym ekranem zewnętrznym i kompaktową konstrukcją.", 4399.00m, "Smartfony"),
        new("Samsung Galaxy A55 5G 256GB", "Popularny średniak Samsung z aluminiową ramką, ekranem Super AMOLED i obsługą 5G.", 1699.00m, "Smartfony"),
        new("Samsung Galaxy A35 5G 128GB", "Smartfon Samsung z ekranem 120 Hz, aparatem 50 MP z OIS i odpornością na pył oraz zachlapania.", 1299.00m, "Smartfony"),
        new("Samsung Galaxy A15 LTE 128GB", "Budżetowy smartfon Samsung z wyświetlaczem AMOLED i baterią 5000 mAh do codziennego użytku.", 699.00m, "Smartfony"),
        new("iPhone 16 Pro 256GB", "Smartfon Apple z czipem A18 Pro, tytanową obudową i zaawansowanym systemem aparatów.", 5499.00m, "Smartfony"),
        new("iPhone 16 128GB", "Smartfon Apple z ekranem Super Retina XDR, przyciskiem akcji i płynnym działaniem na co dzień.", 3999.00m, "Smartfony"),
        new("Google Pixel 9 Pro 256GB", "Smartfon Google z aparatem AI, jasnym ekranem OLED i czystym systemem Android.", 4799.00m, "Smartfony"),
        new("Google Pixel 8a 128GB", "Smartfon Google z świetnym aparatem obliczeniowym i kompaktowym rozmiarem.", 2199.00m, "Smartfony"),

        // Tablety
        new("Samsung Galaxy Tab S9 Ultra 256GB", "Ogromny tablet Samsung z ekranem 14,6 cala Dynamic AMOLED 2X i rysikiem S Pen w zestawie.", 4999.00m, "Tablety"),
        new("Samsung Galaxy Tab S9+ 256GB", "Tablet Samsung z ekranem 12,4 cala, obsługą DeX i głośnikami stereo AKG.", 3899.00m, "Tablety"),
        new("Samsung Galaxy Tab S9 128GB", "Wszechstronny tablet Samsung 11 cali z wodoszczelnością IP68 i rysikiem S Pen.", 3199.00m, "Tablety"),
        new("Samsung Galaxy Tab S9 FE 128GB", "Tablet Samsung do nauki i notatek z ekranem 90 Hz i rysikiem S Pen w komplecie.", 1899.00m, "Tablety"),
        new("Samsung Galaxy Tab A9+ 128GB", "Przystępny cenowo tablet Samsung 11 cali do filmów, przeglądania sieci i multimediów.", 999.00m, "Tablety"),
        new("Apple iPad Air 11\" M2 128GB", "Lekki tablet Apple z czipem M2, obsługą Apple Pencil Pro i świetną wydajnością.", 2999.00m, "Tablety"),
        new("Apple iPad 10. gen 64GB", "Uniwersalny tablet Apple z ekranem Liquid Retina 10,9 cala do nauki i rozrywki.", 1699.00m, "Tablety"),

        // Laptopy
        new("Samsung Galaxy Book4 Pro 14\"", "Smukły laptop Samsung z ekranem AMOLED, lekką obudową i długim czasem pracy na baterii.", 5299.00m, "Laptopy"),
        new("Samsung Galaxy Book4 15,6\"", "Uniwersalny laptop Samsung z pełnowymiarową klawiaturą i ekranem Full HD do pracy biurowej.", 3199.00m, "Laptopy"),
        new("Apple MacBook Air 13\" M3 16GB/512GB", "Cichy laptop Apple z czipem M3, aluminiową obudową i baterią do 18 godzin.", 5499.00m, "Laptopy"),
        new("Apple MacBook Pro 14\" M3 Pro 18GB/512GB", "Wydajny laptop Apple dla twórców i programistów z ekranem Liquid Retina XDR.", 9499.00m, "Laptopy"),
        new("Lenovo ThinkPad E14 Gen 6", "Biznesowy laptop Lenovo z wygodną klawiaturą, matowym ekranem i złączami do biura.", 3899.00m, "Laptopy"),
        new("Lenovo IdeaPad Slim 5 16\"", "Smukły laptop Lenovo z dużym ekranem 16 cali do pracy zdalnej i multimediów.", 3299.00m, "Laptopy"),
        new("ASUS Vivobook 16", "Laptop ASUS z ekranem 16:10, wygodną klawiaturą i solidnym procesorem.", 2999.00m, "Laptopy"),
        new("ASUS ROG Zephyrus G14", "Kompaktowy laptop gamingowy ASUS z mocną kartą graficzną i ekranem o wysokim odświeżaniu.", 7299.00m, "Laptopy"),

        // Audio
        new("Samsung Galaxy Buds3 Pro", "Bezprzewodowe słuchawki dokanałowe Samsung z aktywną redukcją szumów ANC i dźwiękiem Hi-Fi.", 899.00m, "Audio"),
        new("Samsung Galaxy Buds3", "Słuchawki bezprzewodowe Samsung z lekką konstrukcją i czystym brzmieniem rozmów.", 649.00m, "Audio"),
        new("Samsung Galaxy Buds2 Pro", "Kompaktowe słuchawki douszne Samsung z dźwiękiem 360 Audio i redukcją hałasu.", 549.00m, "Audio"),
        new("Sony WH-1000XM5", "Nauszne słuchawki bezprzewodowe Sony z jedną z najlepszych redukcji hałasu na rynku.", 1399.00m, "Audio"),
        new("Sony WF-1000XM5", "Bezprzewodowe słuchawki douszne Sony z wysoką jakością dźwięku i ANC.", 1099.00m, "Audio"),
        new("Apple AirPods Pro 2", "Douszne słuchawki Apple z adaptacyjnym ANC, trybem kontaktu i etui USB-C.", 1049.00m, "Audio"),
        new("JBL Charge 5", "Przenośny głośnik Bluetooth JBL z mocnym basem, odpornością IP67 i powerbankiem.", 649.00m, "Audio"),
        new("JBL Flip 6", "Kompaktowy głośnik bezprzewodowy JBL idealny w podróż i na wyjazdy.", 479.00m, "Audio"),

        // Smartwatche
        new("Samsung Galaxy Watch Ultra 47mm", "Wytrzymały smartwatch Samsung z tytanową kopertą, dwuzakresowym GPS i długim czasem pracy.", 2699.00m, "Smartwatche"),
        new("Samsung Galaxy Watch7 44mm", "Smartwatch Samsung z szybkim procesorem, czujnikami zdrowia i ekranem Super AMOLED.", 1399.00m, "Smartwatche"),
        new("Samsung Galaxy Watch7 40mm", "Mniejszy smartwatch Samsung 40 mm z funkcjami monitorowania snu i treningów.", 1249.00m, "Smartwatche"),
        new("Samsung Galaxy Watch6 Classic 47mm", "Klasyczny smartwatch Samsung z obrotowym pierścieniem i elegancką kopertą.", 1499.00m, "Smartwatche"),
        new("Apple Watch Series 10 46mm", "Smartwatch Apple z dużym wyświetlaczem OLED, smukłą obudową i funkcjami zdrowotnymi.", 2199.00m, "Smartwatche"),
        new("Garmin Forerunner 265", "Zegarek sportowy Garmin z ekranem AMOLED, zaawansowaną analityką treningu i GPS.", 1899.00m, "Smartwatche"),

        // Monitory
        new("Samsung Odyssey OLED G6 27\"", "Monitor gamingowy Samsung z matrycą OLED, odświeżaniem 360 Hz i błyskawicznym czasem reakcji.", 3499.00m, "Monitory"),
        new("Samsung Odyssey G5 34\" Ultrawide", "Zakrzywiony monitor ultrawide Samsung 34 cale do gier i wielozadaniowej pracy.", 1699.00m, "Monitory"),
        new("Samsung ViewFinity S8 27\" 4K", "Monitor biurowy Samsung 4K UHD z wiernym odwzorowaniem kolorów i portem USB-C.", 1599.00m, "Monitory"),
        new("Dell UltraSharp U2724D 27\" QHD", "Monitor biurowy Dell 27 cali QHD z odświeżaniem 120 Hz i ergonomiczną podstawą.", 1599.00m, "Monitory"),
        new("LG UltraGear 27GN800-B 27\"", "Monitor gamingowy LG QHD IPS 144 Hz z niskim input lagiem.", 1199.00m, "Monitory"),

        // Akcesoria
        new("Samsung T7 Shield 1TB SSD", "Zewnętrzny dysk SSD Samsung 1 TB odporny na wstrząsy i pył, z szybkim transferem USB 3.2.", 439.00m, "Akcesoria"),
        new("Samsung 990 PRO 1TB NVMe M.2", "Szybki dysk SSD NVMe Samsung PCIe 4.0 do komputerów PC i konsol PS5.", 449.00m, "Akcesoria"),
        new("Samsung Ładowarka sieciowa 45W USB-C", "Oryginalna ładowarka Samsung 45W z obsługą Super Fast Charging 2.0.", 149.00m, "Akcesoria"),
        new("Samsung Bezprzewodowa ładowarka Duo", "Ładowarka indukcyjna Samsung pozwalająca ładować jednocześnie smartfon i smartwatch/słuchawki.", 229.00m, "Akcesoria"),
        new("Logitech MX Master 3S", "Ergonomiczna mysz bezprzewodowa Logitech z cichym kliknięciem i rolką MagSpeed.", 429.00m, "Akcesoria"),
        new("Keychron K2 Pro", "Mechaniczna klawiatura bezprzewodowa Keychron z podświetleniem i wsparciem QMK/VIA.", 469.00m, "Akcesoria"),

        // Dom i AGD
        new("Samsung Bespoke Jet AI", "Bezprzewodowy odkurzacz pionowy Samsung z dużą mocą ssania i stacją czyszczącą.", 3299.00m, "Dom i AGD"),
        new("Samsung WindFree Avant oczyszczacz powietrza", "Oczyszczacz powietrza Samsung z cichą pracą i obsługą w aplikacji SmartThings.", 1499.00m, "Dom i AGD"),
        new("Philips Hue White & Color E27", "Inteligentna żarówka LED Philips sterowana aplikacją z pełną paletą kolorów.", 199.00m, "Dom i AGD"),
        new("Xiaomi Robot Vacuum X20+", "Robot sprzątający Xiaomi z funkcją mopowania, stacją opróżniającą i nawigacją laserową.", 1799.00m, "Dom i AGD")
    ];
}