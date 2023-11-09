import { useCreateTerminal, useGetTerminal, useUpdateTerminal } from "api/terminal.queries";
import { useParams } from "react-router-dom";
import { AttributeTypeReferenceView } from "types/common/attributeTypeReferenceView";
import { RdlClassifier } from "types/common/rdlClassifier";
import { RdlPurpose } from "types/common/rdlPurpose";
import { FormMode } from "types/formMode";
import { InfoItem } from "types/infoItem";
import { Direction } from "types/terminals/direction";
import { RdlMedium } from "types/terminals/rdlMedium";
import { TerminalTypeRequest } from "types/terminals/terminalTypeRequest";
import { TerminalView } from "types/terminals/terminalView";

export const useTerminalQuery = () => {
  const { id } = useParams();
  return useGetTerminal(id);
};

export const useTerminalMutation = (id?: string, mode?: FormMode) => {
  const createMutation = useCreateTerminal();
  const updateMutation = useUpdateTerminal(id ?? "");
  return mode === "edit" ? updateMutation : createMutation;
};

export interface TerminalFormFields
  extends Omit<TerminalTypeRequest, "classifierIds" | "purposeId" | "mediumId" | "attributes"> {
  classifiers?: RdlClassifier[];
  purpose?: RdlPurpose;
  medium?: RdlMedium;
  attributes?: AttributeTypeReferenceView[];
}

export const toTerminalFormFields = (terminal: TerminalView): TerminalFormFields => ({
  ...terminal,
  classifiers: terminal.classifiers.length === 0 ? undefined : terminal.classifiers,
  attributes: terminal.attributes.length === 0 ? undefined : terminal.attributes,
});

export const toTerminalTypeRequest = (terminalFormFields: TerminalFormFields): TerminalTypeRequest => ({
  ...terminalFormFields,
  description: terminalFormFields.description ? terminalFormFields.description : undefined,
  classifierIds: terminalFormFields.classifiers ? terminalFormFields.classifiers.map((x) => x.id) : [],
  purposeId: terminalFormFields.purpose?.id,
  notation: terminalFormFields.notation ? terminalFormFields.notation : undefined,
  symbol: terminalFormFields.symbol ? terminalFormFields.symbol : undefined,
  mediumId: terminalFormFields.medium?.id,
  attributes: terminalFormFields.attributes
    ? terminalFormFields.attributes.map((x) => ({ ...x, attributeId: x.attribute.id }))
    : [],
});

export const createDefaultTerminalFormFields = (): TerminalFormFields => ({
  name: "",
  qualifier: Direction.Bidirectional,
});

export const mediumInfoItem = (medium: RdlMedium): InfoItem => ({
  id: medium.id.toString(),
  name: medium.name,
  descriptors: {
    Description: medium.description,
    IRI: medium.iri,
  },
});
