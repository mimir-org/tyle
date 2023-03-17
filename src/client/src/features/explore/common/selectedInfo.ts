type SelectedInfoType = "node" | "terminal";

/**
 * Interface for describing the currently selected item in the search list.
 *
 * @property id uniquely identifies the selected item
 * @property type describes what type of object the id belongs to
 */
export interface SelectedInfo {
  id?: number;
  type?: SelectedInfoType;
}
