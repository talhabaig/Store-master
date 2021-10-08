//AppHostPrefix: It is defined in all master page files

BMS.AppVar = {
    AppHost: document.location.protocol + "//" + document.location.host + AppHostPrefix,
    AppSetting: {
        IsNavigationMenuPinned: true        
    },
    SaveAppSetting: function () {       
        localStorage.setItem('BMS.AppVar.AppSetting', JSON.stringify(BMS.AppVar.AppSetting));
    }
}

if (localStorage.getItem("BMS.AppVar.AppSetting")) {
    BMS.AppVar.AppSetting = JSON.parse(localStorage.getItem("BMS.AppVar.AppSetting"));
}