import { Aspect } from "types/common/aspect";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";
import { RdlClassifier } from "types/common/rdlClassifier";
import { RdlPurpose } from "types/common/rdlPurpose";
import { Direction } from "types/terminals/direction";
import { RdlMedium } from "types/terminals/rdlMedium";
import { TerminalTypeRequest } from "types/terminals/terminalTypeRequest";
import { TerminalView } from "types/terminals/terminalView";

export interface TerminalFormFields {
  name: string;
  description: string;
  classifiers: RdlClassifier[];
  purpose: RdlPurpose | null;
  notation: string;
  aspect: Aspect | null;
  medium: RdlMedium | null;
  qualifier: Direction;
  attributes: AttributeTypeReferenceView[];
}

export const createEmptyTerminalFormFields = (): TerminalFormFields => ({
  name: "",
  description: "",
  classifiers: [],
  purpose: null,
  notation: "",
  aspect: null,
  medium: null,
  qualifier: Direction.Bidirectional,
  attributes: [],
});

export const toTerminalFormFields = (terminal: TerminalView): TerminalFormFields => ({
  ...terminal,
  description: terminal.description ?? "",
  notation: terminal.notation ?? "",
});

export const toTerminalTypeRequest = (terminalFormFields: TerminalFormFields): TerminalTypeRequest => ({
  ...terminalFormFields,
  description: terminalFormFields.description ? terminalFormFields.description : null,
  classifierIds: terminalFormFields.classifiers.map((x) => x.id),
  purposeId: terminalFormFields.purpose?.id ?? null,
  notation: terminalFormFields.notation ? terminalFormFields.notation : null,
  mediumId: terminalFormFields.medium?.id ?? null,
  attributes: terminalFormFields.attributes.map((x) => ({ ...x, attributeId: x.attribute.id })),
});
