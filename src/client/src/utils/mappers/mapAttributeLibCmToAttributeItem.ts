import { darken, mix } from "polished";
import { AttributeItem } from "../../content/home/types/AttributeItem";
import { AttributeLibCm } from "../../models/tyle/client/attributeLibCm";
import { Discipline } from "../../models/tyle/enums/discipline";
import { getColorFromAspect } from "../getColorFromAspect";

export const mapAttributeLibCmToAttributeItem = (attribute: AttributeLibCm): AttributeItem => ({
  id: attribute.id,
  name: attribute.name,
  color: getColorForAttributeLibCm(attribute),
  traits: {
    condition: attribute.attributeCondition,
    qualifier: attribute.attributeQualifier,
    source: attribute.attributeSource,
  },
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
      return "rgba(0, 0, 0, 0)";
    case Discipline.ProjectManagement:
      return "hsl(284,100%,50%)";
    case Discipline.Electrical:
      return "hsl(60,100%,50%)";
    case Discipline.Automation:
      return "hsl(108,100%,50%)";
    case Discipline.Structural:
      return "hsl(35,100%,50%)";
    case Discipline.Operation:
      return "hsl(0,100%,50%)";
    case Discipline.Process:
      return "hsl(180,100%,50%)";
  }
};

/**
 * [POC]
 * Temporary color helper to create some visual differentiation while the color specification is completed
 *
 * @param item
 */
const getColorForAttributeLibCm = (item: AttributeLibCm) =>
  darken(0.25, mix(0.35, getColorFromAspect(item.aspect), getColorFromDiscipline(item.discipline)));
