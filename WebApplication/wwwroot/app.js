(function () {
    'use strict';

    function createElement(tagName, className) {
        var element = document.createElement(tagName);
        element.setAttribute('class', className);
        return element;
    }

    var App = function App() {
        this.initialize();
    };
    App.prototype.initialize = function () {
        this.bindViews();
        this.initViews();
    };
    App.prototype.bindViews = function () {
        this.menu_ = document.getElementById('menu');
    };
    App.prototype.initViews = function () {
        if (this.menu_) {
            this.menu_.addEventListener('click', this.onMenuClicked.bind(this));
        }
    };
    App.prototype.onMenuClicked = function () {
        this.showModal();
    };
    App.prototype.showModal = function () {

        var modal = createElement('div', 'modal');
        var modalMask = createElement('div', 'modal-overlay');
        var modalContent = createElement('div', 'modal-content');

        var feedback = createElement('div', '');
        var feedbackButton = createElement('button', 'button modal-item-button');
        feedbackButton.innerText = '反馈';
        feedback.append(feedbackButton);
        feedback.addEventListener('click', this.onFeedBack);
        modalContent.append(feedback);

        var cancel = createElement('div', '');
        var cancelButton = createElement('button', 'button modal-item-button');
        cancel.append(cancelButton);
        cancelButton.innerText = '取消';
        cancel.addEventListener('click', this.onDismissModal.bind(this));
        modalContent.append(cancel);


        modal.append(modalMask, modalContent);
        document.body.append(modal);

        this.modal_ = modal;
    };
    App.prototype.onDismissModal = function () {
        if (this.modal_) {
            this.modal_.remove();
            this.modal_ = null;
        }
    };
    App.prototype.onFeedBack = function () {

        window.location = window.location.origin + "/help/feedback";
    };
    Window['App'] = new App();
})();

(function () {
    var Search = function Search() {
        this.initialize();
    };
    Search.prototype.initialize = function () {
        this.bindViews();
        this.initViews();
    };
    Search.prototype.bindViews = function () {
        this.element_ = document.getElementById('header-bar');
        this.search_ = document.getElementById('search-btn');
        this.header_ = this.element_.querySelector('header');
        this.headerContent_ = this.element_.querySelector('.header-bar-content');
        this.form_ = this.element_.querySelector('.search-mode');

    };
    Search.prototype.initViews = function () {
        this.search_.addEventListener('click', this.onSearch.bind(this));
    };
    Search.prototype.addClose = function () {
        var btn = document.createElement('button');
        jq.html(btn, "<c3-icon flip-for-rtl=\"true\"><svg viewBox=\"0 0 24 24\" preserveAspectRatio=\"xMidYMid meet\"><path d=\"M0 0h24v24H0z\" fill=\"none\"></path><path d=\"M20 11H7.83l5.59-5.59L12 4l-8 8 8 8 1.41-1.41L7.83 13H20v-2z\"></path></svg></c3-icon>");
        jq.addClass(btn, 'icon-button');
        jq.attr(btn, 'aria-label', '关闭搜索功能');
        jq.attr(btn, 'aria-haspopup', 'false');

        this.close_ = btn;
        jq.onClick(btn, this.onClose.bind(this));
        this.header_.insertBefore(btn, this.form_);

    };
    Search.prototype.onClose = function () {
        this.overlay_ && this.overlay_.remove();
        this.close_ && this.close_.remove();
        jq.toggleClass(this.headerContent_, 'non-search-mode');
        jq.removeAttr(this.header_, 'data-mode');
        this.toggleLogo();
    };
    Search.prototype.addOverlay = function () {
        var overlay = document.createElement('div');
        jq.attr(overlay, 'class', 'overlay');
        jq.html(overlay, "<button class=\"overlay-button\" aria-label=\"关闭搜索功能\"></button>");
        this.overlay_ = overlay;
        jq.onClick(overlay, this.onClose.bind(this));
        this.element_.insertBefore(overlay, this.header_);
    };

    Search.prototype.toggleLogo = function () {

        var logo = this.element_.querySelector('.header-bar-endpoint');
        if (logo) {
            logo.remove();
        } else {

            logo = document.createElement('a');
            jq.addClass(logo, 'header-bar-endpoint cbox');
            jq.attr(logo, 'aria-label', '首页');
            jq.attr('href', '/');
            jq.attr('id', 'home-icon');
            jq.html(logo, '<c3-icon class="header-bar-logo" flip-for-rtl="false"> <svg viewBox="0 0 68 68" preserveAspectRatio="xMidYMid meet"><path d="M66.5,7.7c-0.8-2.9-2.5-5.4-5.4-6.2C55.8,0.1,34,0,34,0S12.2,0.1,6.9,1.5C4,2.3,2.3,4.8,1.5,7.7C0.1,13,0,24,0,24s0.1,11,1.5,16.3c0.8,2.9,2.5,5.4,5.4,6.2C12.2,47.9,34,48,34,48s21.8-0.1,27.1-1.5c2.9-0.8,4.6-3.3,5.4-6.2C67.9,35,68,24,68,24S67.9,13,66.5,7.7z M27,34V14l18,10L27,34z" transform="translate(0, 10)"></path></svg> </c3-icon>');

            jq.prepend(this.header_, logo);
        }
    };
    Search.prototype.onSearch = function () {

        this.toggleLogo();

        this.addOverlay();
        this.addClose();


        jq.attr(this.header_, 'data-mode', 'searching');
        jq.removeClass(this.headerContent_, 'non-search-mode');
    };


    window['Search'] = new Search();
})();