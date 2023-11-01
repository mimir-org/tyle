import { MimirorgPermission, MimirorgUserCm } from "@mimirorg/typelibrary-types";
import { AttributeView } from "common/types/attributes/attributeView";
import { BlockItem } from "common/types/blockItem";
import { BlockTerminalItem } from "common/types/blockTerminalItem";
import { BlockView } from "common/types/blocks/blockView";
import { TerminalTypeReferenceView } from "common/types/blocks/terminalTypeReferenceView";
import { RdlPurpose } from "common/types/common/rdlPurpose";
import { State } from "common/types/common/state";
import { InfoItem } from "common/types/infoItem";
import { TerminalItem } from "common/types/terminalItem";
import { TerminalView } from "common/types/terminals/terminalView";
import { UserItem } from "common/types/userItem";
import { getColorFromAspect } from "common/utils/getColorFromAspect";
import { Option, getOptionsFromEnum } from "utils";

export const purposeInfoItem = (purpose: RdlPurpose): InfoItem => ({
  id: purpose.id.toString(),
  name: purpose.name,
  descriptors: {
    Description: purpose.description,
    IRI: purpose.iri,
  },
});

export const toTerminalItem = (terminal: TerminalView): TerminalItem => {
  const states = getOptionsFromEnum(State);
  const currentStateLabel = states[terminal.state].label;

  return {
    id: terminal.id,
    name: terminal.name,
    description: terminal.description ?? "",
    color: getColorFromAspect(terminal.aspect),
    attributes: sortInfoItems(mapAttributeViewsToInfoItems(terminal.attributes.map((x) => x.attribute))),
    tokens: [currentStateLabel],
    kind: "TerminalItem",
    state: terminal.state,
    createdBy: terminal.createdBy,
  };
};

export const toBlockItem = (block: BlockView): BlockItem => {
  const states = getOptionsFromEnum(State);
  const currentStateLabel = states[block.state].label;

  return {
    id: block.id,
    name: block.name,
    img: block.symbol ?? "",
    description: block.description ?? "",
    color: getColorFromAspect(block.aspect),
    tokens: [block.version, currentStateLabel],
    terminals: sortBlockTerminals(mapBlockTerminalLibCmsToBlockTerminalItems(block.terminals)),
    attributes: sortInfoItems(mapAttributeViewsToInfoItems(block.attributes.map((x) => x.attribute))),
    kind: "BlockItem",
    state: block.state,
    createdBy: block.createdBy,
  };
};

const mapBlockTerminalLibCmsToBlockTerminalItems = (terminals: TerminalTypeReferenceView[]): BlockTerminalItem[] =>
  terminals.map((x) => ({
    id: x.terminal.id,
    name: x.terminal.name,
    color: getColorFromAspect(x.terminal.aspect),
    maxQuantity: x.maxCount,
    direction: x.direction,
    attributes: sortInfoItems(mapAttributeViewsToInfoItems(x.terminal.attributes.map((x) => x.attribute))),
  }));

const sortBlockTerminals = (terminals: BlockTerminalItem[]) =>
  [...terminals].sort(
    (a, b) => a.direction.toString().localeCompare(b.direction.toString()) || a.name.localeCompare(b.name),
  );

export const mapMimirorgUserCmToUserItem = (user: MimirorgUserCm): UserItem => {
  const permissionOptions = getOptionsFromEnum<MimirorgPermission>(MimirorgPermission);
  const permissionsMap: { [key: string]: Option<MimirorgPermission> } = {};

  Object.keys(user.permissions).forEach((companyId) => {
    const permissionForCompany = user.permissions[Number(companyId)];
    const permissionLabel = permissionOptions.find((x) => x.value === permissionForCompany)?.label;

    if (permissionLabel) {
      permissionsMap[companyId] = { value: permissionForCompany, label: permissionLabel };
    }
  });

  return {
    id: user.id,
    name: `${user.firstName} ${user.lastName}`,
    email: user.email,
    purpose: user.purpose,
    permissions: permissionsMap,
    company: {
      id: user.companyId,
      name: user.companyName,
    },
  };
};

export const mapAttributeViewToInfoItem = (attribute: AttributeView): InfoItem => {
  const infoItem = {
    id: attribute.id,
    name: attribute.name,
    descriptors: {
      Predicate: attribute.predicate?.iri,
    },
  };

  const attributeHasUnits = attribute.units.length > 0;
  if (attributeHasUnits) {
    return {
      ...infoItem,
      descriptors: {
        ...infoItem.descriptors,
        Units: attribute.units
          .map((x) => x.name)
          .sort((a, b) => a.localeCompare(b))
          .join(", "),
      },
    };
  }

  return infoItem;
};

export const mapAttributeViewsToInfoItems = (attributes: AttributeView[]): InfoItem[] =>
  attributes.map(mapAttributeViewToInfoItem);

export const sortInfoItems = (descriptors: InfoItem[]) => [...descriptors].sort((a, b) => a.name.localeCompare(b.name));
