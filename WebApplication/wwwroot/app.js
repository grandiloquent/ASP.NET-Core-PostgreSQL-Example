(function () {
    'use strict';

    function createElement(tagName, className) {
        var element = document.createElement(tagName);
        element.setAttribute('class', className);
        return element;
    }

    var App = function App() {
        this.initialize();
        this.showModal();
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