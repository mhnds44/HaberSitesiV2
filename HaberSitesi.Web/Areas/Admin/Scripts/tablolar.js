$(document).ready(function () {
    // kategori tablo butonları
    //--------------------------------------------------------------------------
    var kategori_tablosu_butonlar = [{
        text: 'Yeni Kayıt',
        iconCls: 'icon-add',
        handler: function () {
            window.location = app_root + 'Admin/Kategori/KategoriEkle';
        }
    }];

    // galeri tablo butonları
    //--------------------------------------------------------------------------
    var galeri_tablosu_butonlar = [{
        text: 'Yeni Kayıt',
        iconCls: 'icon-add',
        handler: function () {
            window.location = app_root + 'Admin/Galeri/GaleriEkle';
        }
    }];

    // etiket tablo butonları
    //--------------------------------------------------------------------------
    var etiket_tablosu_butonlar = [{
        text: 'Yeni Kayıt',
        iconCls: 'icon-add',
        handler: function () {
            window.location = app_root + 'Admin/Etiket/EtiketEkle';
        }
    }];

    // rol tablo butonları
    //--------------------------------------------------------------------------
    var rol_tablosu_butonlar = [{
        text: 'Yeni Kayıt',
        iconCls: 'icon-add',
        handler: function () {
            window.location = app_root + 'Admin/Rol/RolEkle';
        }
    }];

    // kullanıcı tablo butonları
    //--------------------------------------------------------------------------
    var kullanici_tablosu_butonlar = [{
        text: 'Yeni Kayıt',
        iconCls: 'icon-add',
        handler: function () {
            window.location = app_root + 'Admin/Kullanici/KullaniciEkle';
        }
    }];

    // haber tablo butonları
    //--------------------------------------------------------------------------
    var haber_tablosu_butonlar = [{
        text: 'Yeni Kayıt',
        iconCls: 'icon-add',
        handler: function () {
            window.location = app_root + 'Admin/Haber/HaberEkle';
        }
    }];

    // kose yazısı tablo butonları
    //--------------------------------------------------------------------------
    var kose_yazisi_tablosu_butonlar = [{
        text: 'Yeni Kayıt',
        iconCls: 'icon-add',
        handler: function () {
            window.location = app_root + 'Admin/KoseYazisi/KoseYazisiEkle';
        }
    }];

    // kategori tablosu
    //--------------------------------------------------------------------------
    $('#kategori_tablosu').datagrid({
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        sortName: 'Id',
        sortOrder: 'desc',
        toolbar: kategori_tablosu_butonlar,
        nowrap: false,
        pageSize: 50,
        url: app_root + 'Admin/Kategori/KategorilerJson',
        columns: [[
            { field: 'Id', title: 'Id', width: 30, sortable: true },
            { field: 'Ad', title: 'Ad', sortable: true },
            { field: 'SeoAd', title: 'Seo Ad', sortable: true },
            { field: 'Aciklama', title: 'Açıklama', width: 200 },
            { field: 'SiraNo', title: 'Sıra No', sortable: true },
            {
                field: 'AnaMenu', title: 'Ana Menu', formatter: function (value, row, index) {
                    if (row.AnaMenu) {
                        return '<a href="' + app_root + 'Admin/Kategori/KategoriDuzeyDegistir?id=' + row.Id + '&durum=' + row.AnaMenu + '" class="tablo_buton_green">Evet</a>';
                    } else {
                        return '<a href="' + app_root + 'Admin/Kategori/KategoriDuzeyDegistir?id=' + row.Id + '&durum=' + row.AnaMenu + '" class="tablo_buton_red">Hayır</a>';
                    }

                }
            },
            {
                field: 'Duzenle', title: 'Düzenle', formatter: function (value, row, index) {
                    return '<a href="' + app_root + 'Admin/Kategori/KategoriDuzenle?id=' + row.Id + '" class="tablo_buton">Düzenle</a>';
                }
            },
                {
                    field: 'Sil', title: 'Sil', formatter: function (value, row, index) {
                        return '<a href="' + app_root + 'Admin/Kategori/KategoriSil?id=' + row.Id + '" class="tablo_buton" onclick="return confirm(\'Silmek istediğinize emin misiniz?\')">Sil</a>';
                    }
                }
        ]]
    });

    // galeri tablosu
    //--------------------------------------------------------------------------
    $('#galeri_tablosu').datagrid({
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        sortName: 'Id',
        sortOrder: 'desc',
        toolbar: galeri_tablosu_butonlar,
        nowrap: false,
        pageSize: 50,
        url: app_root + 'Admin/Galeri/GalerilerJson',
        columns: [[
            { field: 'Id', title: 'Id', width: 30, sortable: true },
            { field: 'Ad', title: 'Ad', width: 150, sortable: true },
            {
                field: 'Duzenle', title: 'Düzenle', formatter: function (value, row, index) {
                    return '<a href="' + app_root + 'Admin/Galeri/GaleriDuzenle?id=' + row.Id + '" class="tablo_buton">Düzenle</a>';
                }
            },
                {
                    field: 'Sil', title: 'Sil', formatter: function (value, row, index) {
                        return '<a href="' + app_root + 'Admin/Galeri/GaleriSil?id=' + row.Id + '" class="tablo_buton" onclick="return confirm(\'Silmek istediğinize emin misiniz?\')">Sil</a>';
                    }
                }
        ]]
    });

    // etiket tablosu
    //--------------------------------------------------------------------------
    $('#etiket_tablosu').datagrid({
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        sortName: 'Id',
        sortOrder: 'desc',
        toolbar: etiket_tablosu_butonlar,
        nowrap: false,
        pageSize: 50,
        url: app_root + 'Admin/Etiket/EtiketlerJson',
        columns: [[
            { field: 'Id', title: 'Id', width: 30, sortable: true },
            { field: 'Ad', title: 'Ad', width: 150, sortable: true },
            {
                field: 'Duzenle', title: 'Düzenle', formatter: function (value, row, index) {
                    return '<a href="' + app_root + 'Admin/Etiket/EtiketDuzenle?id=' + row.Id + '" class="tablo_buton">Düzenle</a>';
                }
            },
                {
                    field: 'Sil', title: 'Sil', formatter: function (value, row, index) {
                        return '<a href="' + app_root + 'Admin/Etiket/EtiketSil?id=' + row.Id + '" class="tablo_buton" onclick="return confirm(\'Silmek istediğinize emin misiniz?\')">Sil</a>';
                    }
                }
        ]]
    });

    // rol tablosu
    //--------------------------------------------------------------------------
    $('#rol_tablosu').datagrid({
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        sortName: 'Id',
        sortOrder: 'desc',
        toolbar: rol_tablosu_butonlar,
        nowrap: false,
        pageSize: 50,
        url: app_root + 'Admin/Rol/RollerJson',
        columns: [[
            { field: 'Id', title: 'Id', width: 30, sortable: true },
            { field: 'Ad', title: 'Ad', width: 150, sortable: true },
            {
                field: 'Duzenle', title: 'Düzenle', formatter: function (value, row, index) {
                    return '<a href="' + app_root + 'Admin/Rol/RolDuzenle?id=' + row.Id + '" class="tablo_buton">Düzenle</a>';
                }
            },
                {
                    field: 'Sil', title: 'Sil', formatter: function (value, row, index) {
                        return '<a href="' + app_root + 'Admin/Rol/RolSil?id=' + row.Id + '" class="tablo_buton" onclick="return confirm(\'Silmek istediğinize emin misiniz?\')">Sil</a>';
                    }
                }
        ]]
    });

    // kullanıcı tablosu
    //--------------------------------------------------------------------------
    $('#kullanici_tablosu').datagrid({
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        sortName: 'Id',
        sortOrder: 'desc',
        //toolbar: kullanici_tablosu_butonlar,
        fitColumns: true,
        nowrap: true,
        pageSize: 50,
        url: app_root + 'Admin/Kullanici/KullanicilarJson',
        columns: [[
            { field: 'Id', title: 'Id', width: 30, sortable: true },
            { field: 'KullaniciAdi', title: 'Kullanıcı Adı', width: 150, sortable: true },
            { field: 'Eposta', title: 'E-posta', width: 150, sortable: true },
            { field: 'Roller', title: 'Roller', width: 150, sortable: true },
            {
                field: 'Duzenle', title: 'Düzenle', formatter: function (value, row, index) {
                    return '<a href="' + app_root + 'Admin/Kullanici/KullaniciRolDuzenle?id=' + row.Id + '" class="tablo_buton">Rol Düzenle</a>';
                }
            },
                {
                    field: 'Sil', title: 'Sil', formatter: function (value, row, index) {
                        return '<a href="' + app_root + 'Admin/Kullanici/KullaniciSil?id=' + row.Id + '" class="tablo_buton" onclick="return confirm(\'Silmek istediğinize emin misiniz?\')">Sil</a>';
                    }
                }
        ]]
    });

    // haber tablosu
    //--------------------------------------------------------------------------
    $('#haber_tablosu').datagrid({
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        sortName: 'Id',
        sortOrder: 'desc',
        toolbar: haber_tablosu_butonlar,
        fitColumns: true,
        nowrap: true,
        pageSize: 50,
        url: app_root + 'Admin/Haber/HaberlerJson',
        columns: [[
            { field: 'Id', title: 'Id', width: 30, sortable: true },
            {
                field: 'Resim', title: 'Resim', formatter: function (value, row, index) {
                    return '<img src="' + app_root + row.Resim + '" class="tablo_resim"/>';
                }
            },
            { field: 'Baslik', title: 'Başlık', width: 150, sortable: true },
            { field: 'Kategori', title: 'Kategori', sortable: true },
            { field: 'Pozisyon', title: 'Pozisyon', sortable: true },
            {
                field: 'Yayinda', title: 'Yayında', formatter: function (value, row, index) {
                    if (row.Yayinda) {
                        return '<a href="' + app_root + 'Admin/Haber/HaberYayinDegistir?id=' + row.Id + '&durum=' + row.Yayinda + '" class="tablo_buton_green">Evet</a>';
                    } else {
                        return '<a href="' + app_root + 'Admin/Haber/HaberYayinDegistir?id=' + row.Id + '&durum=' + row.Yayinda + '" class="tablo_buton_red">Hayır</a>';
                    }

                }
            },
            {
                field: 'Duzenle', title: 'Düzenle', formatter: function (value, row, index) {
                    return '<a href="' + app_root + 'Admin/Haber/HaberDuzenle?id=' + row.Id + '" class="tablo_buton">Düzenle</a>';
                }
            },
                {
                    field: 'Sil', title: 'Sil', formatter: function (value, row, index) {
                        return '<a href="' + app_root + 'Admin/Haber/HaberSil?id=' + row.Id + '" class="tablo_buton" onclick="return confirm(\'Silmek istediğinize emin misiniz?\')">Sil</a>';
                    }
                }
        ]]
    });

    // kose yazısı tablosu
    //--------------------------------------------------------------------------
    $('#kose_yazisi_tablosu').datagrid({
        rownumbers: true,
        singleSelect: true,
        pagination: true,
        sortName: 'Id',
        sortOrder: 'desc',
        toolbar: kose_yazisi_tablosu_butonlar,
        fitColumns: true,
        nowrap: true,
        pageSize: 50,
        url: app_root + 'Admin/KoseYazisi/KoseYazilariJson',
        columns: [[
            { field: 'Id', title: 'Id', width: 30, sortable: true },
            {
                field: 'Resim', title: 'Resim', formatter: function (value, row, index) {
                    return '<img src="' + app_root + row.Resim + '" class="tablo_yazar_resim"/>';
                }
            },
            { field: 'Baslik', title: 'Başlık', width: 150, sortable: true },
            { field: 'Kategori', title: 'Kategori', width: 150, sortable: true },
            {
                field: 'Yayinda', title: 'Yayında', formatter: function (value, row, index) {
                    if (row.Yayinda) {
                        return '<a href="' + app_root + 'Admin/KoseYazisi/KoseYazisiYayinDegistir?id=' + row.Id + '&durum=' + row.Yayinda + '" class="tablo_buton_green">Evet</a>';
                    } else {
                        return '<a href="' + app_root + 'Admin/KoseYazisi/KoseYazisiYayinDegistir?id=' + row.Id + '&durum=' + row.Yayinda + '" class="tablo_buton_red">Hayır</a>';
                    }

                }
            },
            {
                field: 'Duzenle', title: 'Düzenle', formatter: function (value, row, index) {
                    return '<a href="' + app_root + 'Admin/KoseYazisi/KoseYazisiDuzenle?id=' + row.Id + '" class="tablo_buton">Düzenle</a>';
                }
            },
                {
                    field: 'Sil', title: 'Sil', formatter: function (value, row, index) {
                        return '<a href="' + app_root + 'Admin/KoseYazisi/KoseYazisiSil?id=' + row.Id + '" class="tablo_buton" onclick="return confirm(\'Silmek istediğinize emin misiniz?\')">Sil</a>';
                    }
                }
        ]]
    });




});