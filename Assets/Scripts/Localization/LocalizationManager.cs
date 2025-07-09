using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class LocalizationManager
{
    // Set the locale thanks to the save file
    public static string locale = "EN";
    
    public static string FileNamePrefix = "GameContent_";
    public static string FileExtension = ".json";
    public static string ContentFile = "";
    
    private static InputActionAsset controls;

    public LocalizationManager(InputActionAsset control)
    {
        controls = control;
    }
    
    /// <summary>
    /// Check if the file exist and if there is a file, try to get the value according to the given key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetContentByKey(string key, bool isWithInput)
    {
        TryGetContentFile();
        if (string.IsNullOrEmpty(ContentFile))
        {
            Debug.LogError("Content file not found.");
            return null;
        }
        try
        {
            string json = File.ReadAllText(ContentFile);

            LocalizationData data = JsonUtility.FromJson<LocalizationData>(json);

            if (data != null && data.Text != null)
            {
                foreach (var entry in data.Text)
                {
                    if (entry.key == key)
                    {
                        // If we want to interact, replace "key" by the target input
                        if (isWithInput)
                        {
                            string InteractionInput = GetInteractKey();
                            string result = entry.Value.Replace("{key}",InteractionInput);
                            return result;
                        }
                        // if we just get a text without input variable
                        return entry.Value;
                    }
                }

                Debug.LogWarning($"Key '{key}' not found in localization file.");
                return null;
            }
            else
            {
                Debug.LogError("Failed to parse localization file.");
                return null;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to read localization file: {ex.Message}");
            return null;
        }
    }
    
    
    public static void TryGetContentFile()
    {
        string fullFileName = FileNamePrefix + locale.ToLower() + FileExtension;
        
        string filePath = Path.Combine(Application.streamingAssetsPath, fullFileName);
        
        // Check if the file exist, if it does, get the content
        if(File.Exists(filePath))
            ContentFile = filePath;
        else
            Debug.LogError($"File {fullFileName} not found");
        
    }
    
    public static string GetInteractKey()
    {
        var playerMap = controls.FindActionMap("Player");
        if (playerMap == null)
        {
            Debug.LogError("Action map 'Player' not found in controls");
            return "?";
        }

        var interactAction = playerMap.FindAction("Interact");
        if (interactAction == null)
        {
            Debug.LogError("Action 'Interact' not found in Player map");
            return "?";
        }

        if (interactAction.bindings.Count > 0)
        {
            return interactAction.bindings[0].ToDisplayString();
        }

        return "?";
    }
}