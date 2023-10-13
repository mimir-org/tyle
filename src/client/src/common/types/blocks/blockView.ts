import { Aspect } from "../common/aspect";
import { AttributeTypeReferenceView } from "../common/attributeTypeReferenceView";
import { ImfType } from "../common/imfType";
import { RdlClassifier } from "../common/rdlClassifier";
import { RdlPurpose } from "../common/rdlPurpose";
import { TerminalTypeReferenceView } from "./terminalTypeReferenceView";

export interface BlockView extends ImfType {
  classifiers: RdlClassifier[];
  purpose?: RdlPurpose;
  notation?: string;
  symbol?: string;
  aspect?: Aspect;
  terminals: TerminalTypeReferenceView[];
  attributes: AttributeTypeReferenceView[];
}
