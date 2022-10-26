import { valueObjectListSchema } from "./valueObjectListSchema";
import { ValueObject } from "../../types/valueObject";

describe("valueObjectListSchema tests", () => {
  it("should reject empty value objects", async () => {
    const emptyValueObjects: ValueObject<string>[] = [{ value: "" }];
    await expect(valueObjectListSchema("Value required").validate(emptyValueObjects)).rejects.toBeTruthy();
  });
});
