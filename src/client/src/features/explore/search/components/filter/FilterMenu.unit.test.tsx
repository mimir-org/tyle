import { afterEach, describe, expect, test } from "vitest";
import { FilterMenu } from "./FilterMenu";
import { cleanup, render, screen } from "@testing-library/react";
import React from "react";
import { MimirorgThemeProvider } from "@mimirorg/component-library";
import "@testing-library/jest-dom";
import { userEvent } from "@testing-library/user-event/setup/index";


const filterGroupsMock = [
  {
    name: "Entity",
    filters: [
      {
        key: "kind",
        label: "Block",
        value: "BlockLibCm"
      },
      {
        key: "kind",
        label: "Terminal",
        value: "TerminalLibCm"
      },
      {
        key: "kind",
        label: "Attribute",
        value: "AttributeLibCm"
      },
      {
        key: "kind",
        label: "Unit",
        value: "UnitLibCm"
      },
      {
        key: "kind",
        label: "Quantity datum",
        value: "QuantityDatumLibCm"
      },
      {
        key: "kind",
        label: "RDS",
        value: "RdsLibCm"
      }
    ]
  },
  {
    name: "Terminal",
    filters: [
      {
        key: "kind",
        label: "Terminal",
        value: "TerminalLibCm"
      }
    ]
  }
];

const activeFiltersMock = [
  {
    key: "kind",
    label: "Block",
    value: "BlockLibCm"
  }
];

const setup = () => {
  const mockToggleFilter = vi.fn();
  const testComponent = render(
    <MimirorgThemeProvider theme={"tyleLight"}>
      <FilterMenu toggleFilter={mockToggleFilter} name={"Filter"} filterGroups={filterGroupsMock}
                  activeFilters={activeFiltersMock} />
    </MimirorgThemeProvider>
  );
  const filterButton = screen.getByRole("button", { name: "Filter" });

  return {
    mockToggleFilter,
    filterButton,
    ...testComponent
  };
};

describe("Filter dropdown menu button tests", () => {
  afterEach(() => {
    cleanup();
  });

  test("Did component render", () => {
    const { filterButton } = setup();
    expect(filterButton).toBeVisible();
  });

  test("Hover color ", () => {
    const { filterButton } = setup();
    userEvent.hover(filterButton);
    //TODO: Dobbeltsjekket at dette er riktig fargekode.
    expect(filterButton).toHaveStyle(`background-color: "#b7f08b"`);
  });
});