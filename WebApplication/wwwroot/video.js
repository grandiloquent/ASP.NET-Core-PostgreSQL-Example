;(function () {

    function getScreenWidth() {
        return window.innerWidth
            || document.documentElement.clientWidth
            || document.body.clientWidth;
    }

    /*
    * https://developer.mozilla.org/en-US/docs/Web/API/HTMLMediaElement
    * 
    * 
    * */
    var Video = function Video() {
        this.initialize();
    };


    Video.prototype.initialize = function () {

        this.bindViews();
        /*this.addSource();*/
        this.increaseWatchTimes();
    };
    Video.prototype.bindViews = function () {

        this.like_ = document.getElementById('like');
        this.unlike_ = document.getElementById('unlike');

        this.player_ = document.getElementById('movie_player');
        this.video_ = this.player_.querySelector('video');

        this.width_ = parseInt(this.video_.getAttribute("data-width"));
        this.height_ = parseInt(this.video_.getAttribute("data-height"));

        this.player_.addEventListener('contextmenu', function (ev) {
            return false
        });
        var that = this;
        window.addEventListener('resize', function (ev) {
            that.setSize();
        });
        jq.onClick(this.like_, this.onLike.bind(this));
        jq.onClick(this.unlike_, this.onLike.bind(this));

    };

    Video.prototype.onLike = function () {
        var id = jq.attr(this.video_, "data-id");
        var that = this;
        fetch("/api/like/" + id, {method: 'post'})
            .then(function (value) {
                return value.text();
            }).then(function (value) {
            that.like_.querySelector('span').innerText = value + "次";
        }).catch(function (reason) {
            console.log(reason);
        })
    };
    Video.prototype.onLike = function () {
        var id = jq.attr(this.video_, "data-id");
        var that = this;
        fetch("/api/unlike/" + id, {method: 'post'})
            .then(function (value) {
                return value.text();
            }).then(function (value) {
            that.unlike_.querySelector('span').innerText = value + "次";
        }).catch(function (reason) {
            console.log(reason);
        })
    };
    Video.prototype.increaseWatchTimes = function () {
        var id = jq.attr(this.video_, "data-id");

        fetch("/api/increase/" + id, {method: 'post'})
            .then(function (value) {
                return value.text();
            }).then(function (value) {
            console.log(value);
        }).catch(function (reason) {
            console.log(reason);
        })
    };
    Video.prototype.addSource = function () {
        this.url_ = this.player_.getAttribute("data-version");
        var source = document.createElement('source');
        source.src = window.location.origin + "/videos/" + this.url_;
        source.type = "video/mp4";
        this.video_.append(source);
    };
    Video.prototype.play = function (url) {


        var video = this.video_;
        var xhr = new XMLHttpRequest();
        xhr.open('GET', window.location.origin + "/videos/" + url, true);
        xhr.responseType = 'blob'; //important
        xhr.onload = function (e) {
            if (this.status === 200) {
                var blob = this.response;

                video.oncanplaythrough = function () {

                    URL.revokeObjectURL(this.src);
                };
                video.src = URL.createObjectURL(blob);
                video.load();
                video.play();
            }
        };
        xhr.send();
        /* this.video_.load();
         this.video_.play();*/
        this.setSize();
        this.video_.style.top = 0;

    };
    Video.prototype.setSize = function () {
        var width = getScreenWidth();
        this.video_.style.width = width + "px";
        this.video_.style.height = Math.round(width * (this.width_ / this.height_)) + "px";
    };
    window['Video'] = new Video();

})();