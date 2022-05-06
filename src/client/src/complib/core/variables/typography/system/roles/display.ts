import { typeScaleSystem } from "../typeScaleSystem";
import { typefaceReference } from "../../reference/typefaceReference";
import { math } from "polished";
import { NominalScale } from "../../types";
import { typeScaleReference } from "../../reference/typeScaleReference";

export const display: NominalScale = {
  large: {
    tracking: 0,
    size: typeScaleSystem.size.p7,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p7,
    letterSpacing: math(`0 / ${typeScaleReference.size.p7} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p7} ${typefaceReference.brand}`,
  },
  medium: {
    tracking: 0,
    size: typeScaleSystem.size.p6,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p6,
    letterSpacing: math(`0 / ${typeScaleReference.size.p6} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p6} ${typefaceReference.brand}`,
  },
  small: {
    tracking: 0,
    size: typeScaleSystem.size.p5,
    family: typefaceReference.brand,
    weight: typefaceReference.weights.normal,
    lineHeight: typeScaleSystem.lineHeight.p5,
    letterSpacing: math(`0 / ${typeScaleReference.size.p5} * 1rem`),
    font: `${typefaceReference.weights.normal} ${typeScaleSystem.size.p5} ${typefaceReference.brand}`,
  },
}