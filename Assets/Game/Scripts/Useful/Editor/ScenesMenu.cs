#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Game.Scripts.Useful.Editor
{
    public class ScenesMenu : EditorWindow
    {
        private GUIStyle simpleButtonStyle;
        private GUIStyle greenButtonStyle;
        private Vector2 scrollPosition = Vector2.zero;

        [MenuItem("GameScenes/Show All Scenes #%&L")]
        public static void ShowWindow()
        {
            GetWindow(typeof(ScenesMenu));
        }

        void OnEnable()
        {
            this.minSize = new Vector2(20, 20);
        }

        void OnGUI()
        {
            if (simpleButtonStyle == null)
            {
                simpleButtonStyle = new GUIStyle(GUI.skin.button);
                simpleButtonStyle.normal.textColor = Color.white;
                simpleButtonStyle.hover.textColor = Color.white;
                simpleButtonStyle.active.textColor = Color.white;
                simpleButtonStyle.alignment = TextAnchor.MiddleLeft;
            }

            if (greenButtonStyle == null)
            {
                greenButtonStyle = new GUIStyle(GUI.skin.button);
                greenButtonStyle.normal.textColor = Color.green;
                greenButtonStyle.hover.textColor = Color.green;
                greenButtonStyle.active.textColor = Color.green;
                greenButtonStyle.alignment = TextAnchor.MiddleLeft;
            }

            GUILayout.Label("Custom Scene Menu", EditorStyles.boldLabel);
            var scenes = EditorBuildSettings.scenes;


            scrollPosition = GUILayout.BeginScrollView(scrollPosition);

            for (int i = 0; i < scenes.Length; i++)
            {
                string sceneName = $"{System.IO.Path.GetFileNameWithoutExtension(scenes[i].path)}";
                GUIStyle style = EditorSceneManager.GetActiveScene().buildIndex == i
                    ? greenButtonStyle
                    : simpleButtonStyle;
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(sceneName, style, GUILayout.ExpandWidth(true)))
                {
                    LoadSceneWithIndex(i);
                }

                if (GUILayout.Button("P", GUILayout.Width(20)))
                {
                    PlaySceneWithIndex(i);
                }

                if (GUILayout.Button("L", GUILayout.Width(20)))
                {
                    SelectScene(i);
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }

        private static string _previousScenePath;

        private static void LoadSceneWithIndex(int index)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorSceneManager.OpenScene(EditorBuildSettings.scenes[index].path);
            }
        }

        private static void PlaySceneWithIndex(int index)
        {
            _previousScenePath = EditorSceneManager.GetActiveScene().path;
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
                EditorSceneManager.OpenScene(EditorBuildSettings.scenes[index].path);
                EditorApplication.isPlaying = true; // ��������� ����
            }
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredEditMode)
            {
                if (!string.IsNullOrEmpty(_previousScenePath))
                {
                    EditorSceneManager.OpenScene(_previousScenePath);
                    _previousScenePath = null;
                    EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
                }
            }
        }


        public static void SelectScene(int index)
        {
            if (index < 0 || index >= EditorBuildSettings.scenes.Length)
            {
                Debug.LogError("Invalid scene index.");
                return;
            }

            string scenePath = EditorBuildSettings.scenes[index].path;

            // Load the asset at the specified path and select it
            Object sceneAsset = AssetDatabase.LoadAssetAtPath<Object>(scenePath);
            if (sceneAsset != null)
            {
                Selection.activeObject = sceneAsset;
                EditorGUIUtility.PingObject(sceneAsset);
            }
            else
            {
                Debug.LogError("Scene asset not found at path: " + scenePath);
            }
        }
    }
}
#endif