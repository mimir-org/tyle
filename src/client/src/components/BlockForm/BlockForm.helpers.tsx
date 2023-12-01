import { BlockTypeRequest } from "types/blocks/blockTypeRequest";
import { BlockView } from "types/blocks/blockView";
import { ConnectionPoint } from "types/blocks/connectionPoint";
import { EngineeringSymbol } from "types/blocks/engineeringSymbol";
import { Aspect } from "types/common/aspect";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";
import { RdlClassifier } from "types/common/rdlClassifier";
import { RdlPurpose } from "types/common/rdlPurpose";
import { Direction } from "types/terminals/direction";

export interface BlockFormFields {
  name: string;
  description: string;
  classifiers: RdlClassifier[];
  purpose: RdlPurpose | null;
  notation: string;
  symbol: EngineeringSymbol | null;
  aspect: Aspect | null;
  terminals: TerminalTypeReferenceField[];
  attributes: AttributeTypeReferenceView[];
}

export interface TerminalTypeReferenceField {
  id: string;
  terminalId: string;
  terminalName: string;
  terminalQualifier: Direction;
  direction: Direction;
  minCount: number;
  maxCount: number | null;
  connectionPoint: ConnectionPoint | null;
}

export const createEmptyBlockFormFields = (): BlockFormFields => ({
  name: "",
  description: "",
  classifiers: [],
  purpose: null,
  notation: "",
  symbol: null,
  aspect: null,
  terminals: [],
  attributes: [],
});

export const toBlockFormFields = (blockView: BlockView): BlockFormFields => ({
  ...blockView,
  description: blockView.description ?? "",
  purpose: blockView.purpose ?? null,
  notation: blockView.notation ?? "",
  symbol: blockView.symbol ?? null,
  aspect: blockView.aspect ?? null,
  terminals: blockView.terminals.map((terminalTypeReferenceView) => ({
    ...terminalTypeReferenceView,
    id: crypto.randomUUID(),
    terminalId: terminalTypeReferenceView.terminal.id,
    terminalName: terminalTypeReferenceView.terminal.name,
    terminalQualifier: terminalTypeReferenceView.terminal.qualifier,
    maxCount: terminalTypeReferenceView.maxCount ?? null,
  })),
});

export const toBlockTypeRequest = (blockFormFields: BlockFormFields): BlockTypeRequest => ({
  ...blockFormFields,
  description: blockFormFields.description ? blockFormFields.description : null,
  classifierIds: blockFormFields.classifiers.map((classifier) => classifier.id),
  purposeId: blockFormFields.purpose?.id ?? null,
  notation: blockFormFields.notation ? blockFormFields.notation : null,
  symbolId: blockFormFields.symbol?.id ?? null,
  terminals: blockFormFields.terminals.map((terminalTypeReferenceField) => ({
    ...terminalTypeReferenceField,
    terminalId: terminalTypeReferenceField.terminalId,
    connectionPointId: terminalTypeReferenceField.connectionPoint?.id ?? null,
  })),
  attributes: blockFormFields.attributes.map((attributeTypeReferenceView) => ({
    ...attributeTypeReferenceView,
    attributeId: attributeTypeReferenceView.attribute.id,
  })),
});
