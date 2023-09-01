using System.Collections.Generic;

[System.Serializable]
public class ChildrenProfile {
    public List<ChildProfile> childProfile = new List<ChildProfile>();
}

[System.Serializable]
public class ChildProfile {
    public int index;
    public string name;
    public int age;
    public string gender;
    public string like;
    public string dislike;
    public string notes;
}