import { MimirorgThemeProvider } from "@mimirorg/component-library";
import "@testing-library/jest-dom";
import { cleanup, render, screen } from "@testing-library/react";
import { afterEach, describe, expect, test } from "vitest";
import FilterMenu from "./FilterMenu";

const filterGroupsMock = [
  {
    name: "Entity",
    filters: [
      {
        key: "kind",
        label: "Block",
        value: "BlockLibCm",
      },
      {
        key: "kind",
        label: "Terminal",
        value: "TerminalLibCm",
      },
      {
        key: "kind",
        label: "Attribute",
        value: "AttributeLibCm",
      },
      {
        key: "kind",
        label: "Unit",
        value: "UnitLibCm",
      },
      {
        key: "kind",
        label: "Quantity datum",
        value: "QuantityDatumLibCm",
      },
      {
        key: "kind",
        label: "RDS",
        value: "RdsLibCm",
      },
    ],
  },
  {
    name: "Terminal",
    filters: [
      {
        key: "kind",
        label: "Terminal",
        value: "TerminalLibCm",
      },
    ],
  },
];

const activeFiltersMock = [
  {
    key: "kind",
    label: "Block",
    value: "BlockLibCm",
  },
];

const setup = () => {
  const testComponent = render(
    <MimirorgThemeProvider theme={"tyleLight"}>
      <FilterMenu
        toggleFilter={() => {}}
        name={"Filter"}
        filterGroups={filterGroupsMock}
        activeFilters={activeFiltersMock}
      />
    </MimirorgThemeProvider>,
  );
  const filterButton = screen.getByRole("button", { name: "Filter" });

  return {
    filterButton,
    ...testComponent,
  };
};

describe("Filter dropdown menu unit tests", () => {
  afterEach(() => {
    cleanup();
  });

  test("Did component render", () => {
    const { filterButton } = setup();
    expect(filterButton).toBeVisible();
  });
});
