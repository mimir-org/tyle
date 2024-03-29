import { AttributeItem } from "types/attributeItem";
import { AttributeView } from "types/attributes/attributeView";
import { UserView } from "types/authentication/userView";
import { BlockItem } from "types/blockItem";
import { BlockTerminalItem } from "types/blockTerminalItem";
import { BlockView } from "types/blocks/blockView";
import { TerminalTypeReferenceView } from "types/blocks/terminalTypeReferenceView";
import { Aspect } from "types/common/aspect";
import { State } from "types/common/state";
import { InfoItem } from "types/infoItem";
import { TerminalItem } from "types/terminalItem";
import { TerminalView } from "types/terminals/terminalView";
import { UserItem } from "types/userItem";
import { getOptionsFromEnum } from "utils";
import { RdlPredicate } from "../types/attributes/rdlPredicate";
import { RdlUnit } from "../types/attributes/rdlUnit";
import { RoleView } from "../types/authentication/roleView";
import { RdlClassifier } from "../types/common/rdlClassifier";
import { RdlPurpose } from "../types/common/rdlPurpose";
import { RoleItem } from "../types/role";
import { RdlMedium } from "../types/terminals/rdlMedium";

const getColorFromAspect = (aspect: Aspect | null) => {
  switch (aspect) {
    case Aspect.Function:
      return "hsl(57,99%,63%)";
    case Aspect.Product:
      return "hsl(184,100%,50%)";
    case Aspect.Location:
      return "hsl(299,100%,50%)";
    default:
      return "hsl(318, 29%, 91%)";
  }
};

export const toTerminalItem = (terminal: TerminalView): TerminalItem => {
  const states = getOptionsFromEnum(State);
  const currentStateLabel = states[terminal.state].label;

  return {
    id: terminal.id,
    name: terminal.name,
    description: terminal.description ?? "",
    color: getColorFromAspect(terminal.aspect ?? null),
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
    symbol: block.symbol,
    description: block.description ?? "",
    color: getColorFromAspect(block.aspect),
    tokens: [block.version, currentStateLabel],
    terminals: sortBlockTerminals(mapTerminalTypeReferenceViewToBlockTerminalItems(block.terminals)),
    attributes: sortInfoItems(mapAttributeViewsToInfoItems(block.attributes.map((x) => x.attribute))),
    kind: "BlockItem",
    state: block.state,
    createdBy: block.createdBy,
  };
};

export const mapTerminalTypeReferenceViewToBlockTerminalItems = (
  terminals: TerminalTypeReferenceView[],
): BlockTerminalItem[] =>
  terminals.map((x) => ({
    id: x.terminal.id,
    name: x.terminal.name,
    color: getColorFromAspect(x.terminal.aspect ?? null),
    minQuantity: x.minCount,
    maxQuantity: x.maxCount ?? undefined,
    direction: x.direction,
    attributes: sortInfoItems(mapAttributeViewsToInfoItems(x.terminal.attributes.map((x) => x.attribute))),
  }));

const sortBlockTerminals = (terminals: BlockTerminalItem[]) =>
  [...terminals].sort(
    (a, b) => a.direction.toString().localeCompare(b.direction.toString()) || a.name.localeCompare(b.name),
  );

export const mapUserViewToUserItem = (user: UserView): UserItem => {
  return {
    id: user.id,
    name: `${user.firstName} ${user.lastName}`,
    email: user.email,
    purpose: user.purpose,
    roles: user.roles,
  };
};

export const mapRoleViewToRoleItem = (role: RoleView): RoleItem => {
  return {
    roleName: role.name,
    roleId: role.id,
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

export const toAttributeItem = (attribute: AttributeView): AttributeItem => {
  return {
    attributeUnits: attribute.units,
    created: attribute.createdOn,
    createdBy: attribute.createdBy,
    id: attribute.id,
    name: attribute.name,
    description: attribute.description ?? "",
    kind: "AttributeItem",
    state: attribute.state,
    symbol: "",
    unitId: "",
    isDefault: false,
  };
};

export const mapRdlPredicateToInfoItem = (predicate: RdlPredicate | null): InfoItem => {
  return {
    id: predicate?.id.toString(),
    name: predicate?.name ? predicate.name : "",
    descriptors: {
      description: predicate?.description,
      iri: predicate?.iri,
    },
  };
};

export const mapRdlMediumToInfoItem = (medium: RdlMedium | null): InfoItem => {
  return {
    id: medium?.id.toString(),
    name: medium?.name ? medium.name : "",
    descriptors: {
      description: medium?.description,
      iri: medium?.iri,
    },
  };
};

export const mapRdlPurposeToInfoItem = (purpose: RdlPurpose | null): InfoItem => {
  return {
    id: purpose?.id.toString(),
    name: purpose?.name ? purpose.name : "",
    descriptors: {
      description: purpose?.description,
      iri: purpose?.iri,
    },
  };
};

export const mapRdlUnitsToInfoItems = (units: RdlUnit[]): InfoItem[] => {
  return (
    units.map((unit) => ({
      id: unit.id.toString(),
      name: unit.name,
      descriptors: {
        description: unit.description,
        iri: unit.iri,
      },
    })) ?? []
  );
};

export const mapRdlClassifiersToInfoItems = (classifiers: RdlClassifier[]): InfoItem[] => {
  return (
    classifiers.map((classifier) => ({
      id: classifier.id.toString(),
      name: classifier.name,
      descriptors: {
        Description: classifier.description,
        IRI: classifier.iri,
      },
    })) ?? []
  );
};

export const isNullUndefinedOrWhitespace = <T>(input: T | null): boolean => {
  if (input === null || input === undefined) {
    return true;
  }
  if (typeof input === "string") {
    return /^\s*$/.test(input);
  }
  return false;
};
