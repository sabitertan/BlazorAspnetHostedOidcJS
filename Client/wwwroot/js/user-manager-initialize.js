window.tryInitializeUserManager = function () {
	var initializeUserManagerInteropScriptElement = document.getElementById("initializeUserManagerInterop");
	if (initializeUserManagerInteroprScriptElement) {
		let script = initializeUserManagerInteropScriptElement.innerHTML || "";
		script = script.replace("<!--!-->", "");
		if (script) {
			window.canInitializeUserManagerInterop = true;
			eval(script);
		}
		delete window.tryInitializeUserManager;
		return true;
	}
	return false;
};
window.UserManager = new Oidc.UserManager({
    authority: "https://demo.identityserver.io",
    client_id: "spa",
    redirect_uri: "https://localhost:5001/signin-callback.html",
    silent_redirect_uri: "https://localhost:5001/silent-renew.html",
    post_logout_redirect_uri: "https://localhost:5001",
    response_type: "code",
    scope: "openid profile email api"
});
// authenticationService.getUser().then(response=>console.log(response))
window.authenticationService = {
    userManager: UserManager,
    getUser: function () {
        return UserManager.getUser().then(
            function (response) {
                return response;
            },
            function (reject) {
                return reject;
            }
        );
    },
    login: function () {
        return UserManager.signinRedirect();
    },
    silentRenewToken: function () {
        return UserManager.signinSilent();
    },
    logout: function () {
        UserManager.signoutRedirect();
    }
};

///////////////////////////////
// events
///////////////////////////////
UserManager.events.addAccessTokenExpiring(function () {
    console.log("token expiring");
    log("token expiring");
});

UserManager.events.addAccessTokenExpired(function () {
    console.log("token expired");
    log("token expired");
});

UserManager.events.addSilentRenewError(function (e) {
    console.log("silent renew error", e.message);
    log("silent renew error", e.message);
});

UserManager.events.addUserLoaded(function (user) {
    console.log("user loaded", user);
    UserManager.getUser().then(function () {
        console.log("getUser loaded user after userLoaded event fired");
    });
});

UserManager.events.addUserUnloaded(function (e) {
    console.log("user unloaded");
});