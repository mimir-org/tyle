import { AttributeLibCm } from "../../models/tyle/client/attributeLibCm";
import { AttributeItem } from "../../content/home/types/AttributeItem";
import { darken, mix } from "polished";
import { getColorFromAspect } from "../getColorFromAspect";
import { Discipline } from "../../models/tyle/enums/discipline";

export const mapAttributeLibsToAttributeItems = (nodeAttributeLibs: AttributeLibCm[]): AttributeItem[] =>
  nodeAttributeLibs.map((x) => ({
    name: x.name,
    color: getColorForAttributeLibCm(x),
    traits: {
      condition: x.attributeCondition,
      qualifier: x.attributeQualifier,
      source: x.attributeSource,
    },
    value: x.units?.[0]?.name,
  }));

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
