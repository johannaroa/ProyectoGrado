using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tree {
	public Trunk trunk;

	public Tree(Trunk new_trunk) {
		trunk = new_trunk;
	}
}

public class Trunk {
	public int id;
	public string name;
	public List<Branch> branchs = new List<Branch> ();

	public Trunk(string new_name, Branch new_branch) {
		name = new_name;
		branchs.Add (new_branch);
	}
}

public class Branch {
	public int id;
	public string name;
	public List<Leaf> leafs = new List<Leaf> ();

	public Branch (string new_name, Leaf new_leaf) {
		name = new_name;
		leafs.Add(new_leaf);
	}
}

public class Leaf {
	public int id;
	public string name;

	public Leaf(string new_name){
		name = new_name;
	}
}
