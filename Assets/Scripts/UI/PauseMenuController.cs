using System;
using UnityEngine;
using UnityEngine.UI;

namespace Yumemonogatari.UI {
        public class PauseMenuController : MonoBehaviour {

                private enum Pages {
                        Map = 0,
                        Log = 1,
                        Inventory = 2,
                        System = 3
                }

                public RectTransform navPointer;

                // these should be assigned in the editor according to the above page enum
                public Transform[] navItems;
                public GameObject[] pages;

                private Pages _currentPage = Pages.Map;
                private GameObject _hud;

                private void Awake() {
                        // pause the game
                        Time.timeScale = 0;
                        _hud = FindObjectOfType<HudController>().gameObject;
                        _hud.SetActive(false);
                }

                private void OnDestroy() {
                        // resume the game
                        Time.timeScale = 1;
                        _hud.SetActive(true);
                }

                private void Update() {
                        // if the user wants to switch pages
                        if(Input.GetButtonUp("Menu Left"))
                                MovePageLeft();
                        else if(Input.GetButtonUp("Menu Right"))
                                MovePageRight();
                        // unpause
                        else if(Input.GetButtonUp("Pause") || Input.GetButtonUp("Cancel"))
                                Destroy(gameObject);
                }

                private void MovePageLeft() {
                        Pages page;
                        if(_currentPage == Pages.Map)
                                page = Pages.System;
                        else
                                page = _currentPage - 1;

                        SwitchPage(page);
                }

                private void MovePageRight() {
                        Pages page;
                        if(_currentPage == Pages.System)
                                page = Pages.Map;
                        else
                                page = _currentPage + 1;

                        SwitchPage(page);
                }

                private void SwitchPage(Pages page) {
                        var pg = (int)page;
                        pages[(int)_currentPage].SetActive(false);
                        pages[pg].SetActive(true);
                        MoveNavPointer(page);
                        _currentPage = page;
                }

                private void MoveNavPointer(Pages page) {
                        navPointer.SetParent(navItems[(int)page]);
                        var pos = navPointer.localPosition;
                        pos.x = 0;
                        navPointer.localPosition = pos;
                }
        }
}