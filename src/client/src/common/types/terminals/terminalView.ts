import { Aspect } from "../common/aspect";
import { AttributeTypeReferenceView } from "../common/attributeTypeReferenceView";
import { ImfType } from "../common/imfType";
import { RdlClassifier } from "../common/rdlClassifier";
import { RdlPurpose } from "../common/rdlPurpose";
import { Direction } from "./direction";
import { RdlMedium } from "./rdlMedium";

export interface TerminalView extends ImfType {
  classifiers: RdlClassifier[];
  purpose: RdlPurpose | undefined;
  notation: string | undefined;
  symbol: string | undefined;
  aspect: Aspect | undefined;
  medium: RdlMedium | undefined;
  qualifier: Direction;
  attributes: AttributeTypeReferenceView[];
}
