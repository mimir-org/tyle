import { Aspect, ConnectorType, ObjectType, PredefinedAttribute, TerminalTypeItem } from "..";
import { CreateId } from "../../helpers";

export const CREATE_LIBRARY_KIND = "CreateLibraryType";

export default interface CreateLibraryType {
  id: string;
  name: string;
  aspect: Aspect;
  objectType: ObjectType;
  purpose: string;
  semanticReference: string;
  rdsId: string;
  terminalTypes: TerminalTypeItem[];
  attributeTypes: string[];
  locationType: string;
  predefinedAttributes: PredefinedAttribute[];
  terminalTypeId: string;
  symbolId: string;
  simpleTypes: string[];
  kind: string;
}

export const defaultCreateLibraryType: CreateLibraryType = {
  id: "",
  name: "",
  aspect: Aspect.NotSet,
  objectType: ObjectType.NotSet,
  purpose: "",
  semanticReference: "",
  rdsId: "",
  terminalTypes: [] as TerminalTypeItem[],
  attributeTypes: [] as string[],
  locationType: "",
  predefinedAttributes: [] as PredefinedAttribute[],
  terminalTypeId: "",
  symbolId: "",
  simpleTypes: [] as string[],
  kind: CREATE_LIBRARY_KIND,
};

export function fromJsonCreateLibraryType(createLibraryTypeJson: CreateLibraryType): CreateLibraryType {
  return ensureValidState({ ...defaultCreateLibraryType, ...createLibraryTypeJson });
}

function ensureValidState(newCreateLibraryType: CreateLibraryType) {
  const createLibraryTypeState = { ...newCreateLibraryType };

  if (!createLibraryTypeState.attributeTypes) createLibraryTypeState.attributeTypes = [];
  if (!createLibraryTypeState.simpleTypes) createLibraryTypeState.simpleTypes = [];

  if (!createLibraryTypeState.terminalTypes) {
    const defaultTerminalTypeItem = {
      number: 1,
      terminalTypeId: createLibraryTypeState.terminalTypeId,
      connectorType: ConnectorType.Input,
    } as TerminalTypeItem;

    createLibraryTypeState.terminalTypes = [
      defaultTerminalTypeItem,
      { ...defaultTerminalTypeItem, connectorType: ConnectorType.Output },
    ];
  }

  // Assign temporary-ids to items for handling CRUD in redux store
  if (createLibraryTypeState.terminalTypes) {
    createLibraryTypeState.terminalTypes = createLibraryTypeState.terminalTypes.map((terminalTypeItem) => ({
      ...terminalTypeItem,
      terminalId: CreateId(),
    }));
  }

  return createLibraryTypeState;
}
