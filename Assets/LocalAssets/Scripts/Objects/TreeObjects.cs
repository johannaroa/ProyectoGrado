using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreePlus {
	public string name;

	public TreePlus(string new_name) {
		name = new_name;
	}
}

public class Trunk {
	public int id;
	public string name;
	public TreePlus tree;

	public Trunk(int new_id, string new_name, TreePlus new_tree) {
		id = new_id;
		name = new_name;
		tree = new TreePlus(new_tree.name);
	}
}

public class Branch {
	public int id;
	public string name;
	public Trunk trunk;

	public Branch (int new_id, string new_name, Trunk new_trunk) {
		id = new_id;
		name = new_name;
		trunk = new Trunk(new_trunk.id, new_trunk.name, new_trunk.tree);
	}
}

public class Leaf {
	public int id;
	public string name;
	public string description;
	public List<Branch> branchs = new List<Branch> ();

	public Leaf(int new_id, string new_name, string new_description, Branch new_branch){
		id = new_id;
		name = new_name;
		description = new_description;
		branchs.Add(new_branch);
	}
}
