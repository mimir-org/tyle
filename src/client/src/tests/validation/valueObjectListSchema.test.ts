import { valueObjectListSchema } from "../../content/forms/common/validation/valueObjectListSchema";
import { ValueObject } from "../../content/forms/types/valueObject";

describe("valueObjectListSchema tests", () => {
  it("should reject empty value objects", async () => {
    const emptyValueObjects: ValueObject<string>[] = [{ value: "" }];
    await expect(valueObjectListSchema("Value required").validate(emptyValueObjects)).rejects.toBeTruthy();
  });
});
