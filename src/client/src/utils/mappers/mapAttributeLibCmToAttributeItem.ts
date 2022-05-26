import { AttributeLibCm } from "../../models/tyle/client/attributeLibCm";
import { AttributeItem } from "../../content/home/types/AttributeItem";
import { darken, mix } from "polished";
import { getColorFromAspect } from "../getColorFromAspect";
import { Discipline } from "../../models/tyle/enums/discipline";

export const mapAttributeLibCmToAttributeItem = (attribute: AttributeLibCm): AttributeItem => ({
  name: attribute.name,
  color: getColorForAttributeLibCm(attribute),
  traits: {
    condition: attribute.attributeCondition,
    qualifier: attribute.attributeQualifier,
    source: attribute.attributeSource,
  },
  value: attribute.units?.[0]?.name,
});

export const mapAttributeLibCmsToAttributeItems = (attributes: AttributeLibCm[]): AttributeItem[] =>
  attributes.map(mapAttributeLibCmToAttributeItem);

/**
 * [POC]
 * Temporary color helper defining unique colors for the different disciplines
 *
 * @param discipline
 */
const getColorFromDiscipline = (discipline: Discipline) => {
  switch (discipline) {
    case Discipline.NotSet:
    case Discipline.None:
      return "";
    case Discipline.ProjectManagement:
      return "#b900ff";
    case Discipline.Electrical:
      return "#ffff00";
    case Discipline.Automation:
      return "#32ff00";
    case Discipline.Structural:
      return "#ff9600";
    case Discipline.Operation:
      return "#ff0000";
    case Discipline.Process:
      return "#00ffff";
  }
};

/**
 * [POC]
 * Temporary color helper to create some visual differentiation while the color specification is completed
 *
 * @param item
 */
const getColorForAttributeLibCm = (item: AttributeLibCm) =>
  darken(0.25, mix(0.25, getColorFromAspect(item.aspect), getColorFromDiscipline(item.discipline)));
