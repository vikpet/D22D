#pragma strict

var objPlayer : GameObject;

var objLimb : GameObject;


function Start () {

AddLimb( objLimb, objPlayer );

}

function AddLimb( BonedObj : GameObject, RootObj : GameObject )

{

var BonedObjects = BonedObj.gameObject.GetComponentsInChildren( SkinnedMeshRenderer );

    for (var SkinnedRenderer : SkinnedMeshRenderer in BonedObjects)

        ProcessBonedObject( SkinnedRenderer, RootObj ); 

}

private function ProcessBonedObject( ThisRenderer : SkinnedMeshRenderer, RootObj : GameObject )

{

    /*      Create the SubObject        */

    var NewObj = new GameObject( ThisRenderer.gameObject.name );

    NewObj.transform.parent = RootObj.transform;

    /*      Add the renderer        */

    NewObj.AddComponent( SkinnedMeshRenderer );

    var NewRenderer = NewObj.GetComponent( SkinnedMeshRenderer );

    /*      Assemble Bone Structure     */

    var MyBones = new Transform[ ThisRenderer.bones.Length ];

    for ( var i=0; i<ThisRenderer.bones.Length; i++ )

        MyBones[ i ] = FindChildByName( ThisRenderer.bones[ i ].name, RootObj.transform );

    /*      Assemble Renderer       */

    NewRenderer.bones = MyBones;

    NewRenderer.sharedMesh = ThisRenderer.sharedMesh;

    NewRenderer.materials = ThisRenderer.materials;

}

 

private function FindChildByName( ThisName : String, ThisGObj : Transform ) : Transform

{

    var ReturnObj : Transform;

    if( ThisGObj.name==ThisName )

        return ThisGObj.transform;

    for (var child : Transform in ThisGObj ) 

    {

        ReturnObj = FindChildByName( ThisName, child );

        if( ReturnObj )

            return ReturnObj;

    }

    return null;

}

function Update () {

}