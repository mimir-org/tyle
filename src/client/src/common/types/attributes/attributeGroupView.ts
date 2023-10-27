import { ImfType } from "../common/imfType";
import { AttributeView } from "./attributeView";

export interface AttributeGroupView extends Omit<ImfType, "version" | "state"> {
    attributes: AttributeView[];
}