import { useCreateTerminal, useGetTerminal, useUpdateTerminal } from "api/terminal.queries";
import { AttributeTypeReferenceView } from "common/types/common/attributeTypeReferenceView";
import { RdlClassifier } from "common/types/common/rdlClassifier";
import { RdlPurpose } from "common/types/common/rdlPurpose";
import { InfoItem } from "common/types/infoItem";
import { Direction } from "common/types/terminals/direction";
import { RdlMedium } from "common/types/terminals/rdlMedium";
import { TerminalTypeRequest } from "common/types/terminals/terminalTypeRequest";
import { TerminalView } from "common/types/terminals/terminalView";
import { useParams } from "react-router-dom";
import { FormMode } from "../../common/types/formMode";

export const useTerminalQuery = () => {
  const { id } = useParams();
  return useGetTerminal(id ?? "");
};

export const useTerminalMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal(id ?? "");
  return mode === "edit" ? updateMutation : createMutation;
};

export interface TerminalFormFields
  extends Omit<TerminalTypeRequest, "classifierIds" | "purposeId" | "mediumId" | "attributes"> {
  classifiers: RdlClassifier[];
  purpose?: RdlPurpose;
  medium?: RdlMedium;
  attributes: AttributeTypeReferenceView[];
}

export const toTerminalFormFields = (terminal: TerminalView): TerminalFormFields => ({
  ...terminal,
});

export const toTerminalTypeRequest = (terminalFormFields: TerminalFormFields): TerminalTypeRequest => ({
  ...terminalFormFields,
  description: terminalFormFields.description ? terminalFormFields.description : undefined,
  classifierIds: terminalFormFields.classifiers.map((x) => x.id),
  purposeId: terminalFormFields.purpose?.id,
  notation: terminalFormFields.notation ? terminalFormFields.notation : undefined,
  symbol: terminalFormFields.symbol ? terminalFormFields.symbol : undefined,
  mediumId: terminalFormFields.medium?.id,
  attributes: terminalFormFields.attributes.map((x) => ({ ...x, attributeId: x.attribute.id })),
});

export const createDefaultTerminalFormFields = (): TerminalFormFields => ({
  name: "",
  classifiers: [],
  qualifier: Direction.Bidirectional,
  attributes: [],
});

export const mediumInfoItem = (medium: RdlMedium): InfoItem => ({
  id: medium.id.toString(),
  name: medium.name,
  descriptors: {
    Description: medium.description,
    IRI: medium.iri,
  },
});

export const classifierInfoItem = (classifier: RdlClassifier): InfoItem => ({
  id: classifier.id.toString(),
  name: classifier.name,
  descriptors: {
    Description: classifier.description,
    IRI: classifier.iri,
  },
});
