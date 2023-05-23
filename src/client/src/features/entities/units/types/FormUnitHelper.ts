export interface FormUnitHelper {
  name: string;
  description: string;
  symbol: string;
  unitId: string;
}

export const createEmptyFormUnitHelper = (): FormUnitHelper => ({
  name: "",
  description: "",
  symbol: "",
  unitId: "",
});
