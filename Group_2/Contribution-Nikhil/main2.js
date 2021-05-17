    import { EffectComposer } from "https://unpkg.com/three@0.120.0/examples/jsm/postprocessing/EffectComposer.js";
    import { RenderPass } from "https://unpkg.com/three@0.120.0/examples/jsm/postprocessing/RenderPass.js";
    import { UnrealBloomPass } from "https://unpkg.com/three@0.120.0/examples/jsm/postprocessing/UnrealBloomPass.js";
    import { OBJLoader } from "https://unpkg.com/three@0.120.0/examples/jsm/loaders/OBJLoader";
    import { GLTFLoader } from "https://unpkg.com/three@0.120.0/examples/jsm/loaders/GLTFLoader";
    import { FBXLoader } from "https://unpkg.com/three@0.120.0/examples/jsm/loaders/FBXLoader";
    import { OrbitControls } from "https://unpkg.com/three@0.120.0/examples/jsm/controls/OrbitControls";

const render = () => renderer.render( scene, camera );
function bindSceneToDOM(){}


var skullmodel = "https://raw.githubusercontent.com/KhronosGroup/glTF-Sample-Models/master/2.0/CesiumMilkTruck/glTF-Binary/CesiumMilkTruck.glb";
var skullmodel = "./assets/out.glb";
const renderer = new THREE.WebGLRenderer({alpha: true,antialias: true });
const scene = new THREE.Scene();
const camera =new THREE.PerspectiveCamera( 90, window.innerWidth / window.innerHeight, 0.1, 100);

var clock = new THREE.Clock();


var ambientLight = new THREE.AmbientLight( 0xffffff,2 );
scene.add( ambientLight );
var pointLight = new THREE.PointLight( 0xffffff,3 );
// scene.add( pointLight );

var container=document.getElementById("world")
var controls = new OrbitControls( camera,renderer.domElement );
controls.addEventListener( 'change', render );


// var geometry = new THREE.SphereGeometry(30, 64, 64);   
// var material = new THREE.MeshStandardMaterial({ color: "#000", roughness: 1 });
// var g= new THREE.SphereGeometry(1, 32, 32)
// var m= new THREE.MeshPhongMaterial( {color: 0x996633, specular: 0x050505, shininess: 100 } )
// var mesh = new THREE.Mesh(g, m);
// scene.add( mesh );


camera.position.set(1, 1, 24 );
renderer.setSize( window.innerWidth, window.innerHeight );
container.appendChild( renderer.domElement );





function sineValue (timeInterval) {
    var d = new Date();
    var n = d.getTime();
    return Math.sin(2*Math.PI * (n%timeInterval/timeInterval));
}

function animate() {
    requestAnimationFrame( animate );
    // var delta = clock.getDelta(); // clock is an instance of THREE.Clock
    for (var i = 0; i < bananaAnimationMixers.length; i++) {
        bananaAnimationMixers[i].update( 0.0144 )
    }
    // mixer.update( 0.0144 );
    camera.position.set(camera.position.x+=0.001/2, camera.position.y, camera.position.z-=Math.abs(sineValue(3000)/200)+0.02);
    renderer.render( scene, camera );
}

var bananaAnimationMixers=[]
const gltfloader = new GLTFLoader();
function addBanana(displacement){
    const modelLoadAwait= gltfloader.load( skullmodel, function ( gltf ) {
        var root=gltf
        var model = gltf.scene ;
        var animations = root.animations;
        var mixer = new THREE.AnimationMixer( model );
        var action = mixer.clipAction( animations[ 0 ] ); // access first animation clip
        model.children[2].rotation.y+=-90/180
        model.position.z+=displacement*3+3
        model.position.x=-1
        bananaAnimationMixers.push(mixer)
        // model.children[2].rotation.x+=20
        // model.children[2].rotation.z+=60
        scene.add( model );
        action.play();
    },null,(err)=> console.log(err));
}


for (var i = -2; i < 3; i++) {
    addBanana(i)
}

// function addDog() {}
// const fontJson = require('https://unpkg.com/three@0.77.0/examples/fonts/gentilis_regular.typeface.json')
// const font = new THREE.Font( fontJson );

function loadWatermark (name,offsetZ)  {
    // const font = response;
    const geoOptions= {
        font: window.font123,
        size: 0.5,
        height: 0.1,
        curveSegments: 1,
        bevelThickness: 0.05,
        bevelSize: 0.05,
        bevelEnabled: true,
    }
    var textGeo = new THREE.TextGeometry( name, geoOptions);
    // textGeo.computeBoundingBox();
    // var material = new THREE.MeshStandardMaterial({color: 0x111111,shininess:100, wireframe:false })
    var material = [
    new THREE.MeshPhongMaterial( { color: '#ffff00', } ),
    new THREE.MeshPhongMaterial( { color: 0x000000,emissive:0x111100 }),
    ];
    var textMesh2 = new THREE.Mesh( textGeo, material );
    textMesh2.position.z+=offsetZ       
    textMesh2.position.y+=1        
    scene.add(textMesh2);
}

const fonturl='https://unpkg.com/three@0.77.0/examples/fonts/gentilis_regular.typeface.json'
const loader = new THREE.FontLoader();
loader.load( fonturl, function ( response ) {
    window.font123=response
    var baseOffset=0.8
    loadWatermark("We Present,\nComputer Graphics Project to\nPrerana Mam !",baseOffset-7)
    loadWatermark("Shish Pal",baseOffset+4)
    loadWatermark("Ajay Verma",  baseOffset+7)
    loadWatermark("Sayantan Roy",    baseOffset+10)
    loadWatermark("Ayush Bharti",  baseOffset+13)
    loadWatermark("Nikhil Swami",   baseOffset+16)
});



animate();

// console.log ( modelLoadAwait)
