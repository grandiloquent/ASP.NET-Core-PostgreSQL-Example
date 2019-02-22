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
        this.bindViews();
    };


    Video.prototype.initialize = function () {
        this.player_ = document.getElementById('movie_player');
        this.video_ = this.player_.querySelector('video');
        this.play_ = this.player_.querySelector('.ytp-large-play-button');
        this.overlay_ = this.player_.querySelector('.ytp-cued-thumbnail-overlay-image');

        var that = this;
        this.play_.addEventListener('click', this.play.bind(this));
        this.url_ = this.player_.getAttribute("data-version");
        console.log(window.location.origin + "/videos/" + this.url_);
        var source = document.createElement('source');

        source.src = window.location.origin + "/videos/" + this.url_;
        source.type = "video/mp4";
        this.video_.append(source);
    };
    Video.prototype.bindViews = function () {
        this.width_ = parseInt(this.video_.getAttribute("data-width"));
        this.height_ = parseInt(this.video_.getAttribute("data-height"));
        console.log(this.width_ / this.height_);
        var that = this;
        window.addEventListener('resize', function (ev) {
            that.setSize();
        })
    };

    Video.prototype.play = function () {

        this.video_.load();
        this.video_.play();
        this.setSize();
        this.video_.style.top = 0;
        this.overlay_.setAttribute('hidden', 'true');
        this.play_.setAttribute('hidden', 'true');

    };
    Video.prototype.setSize = function () {
        var width = getScreenWidth();
        this.video_.style.width = width + "px";
        this.video_.style.height = Math.round(width / (this.width_ / this.height_)) + "px";
    };
    window['Video'] = new Video();

})();