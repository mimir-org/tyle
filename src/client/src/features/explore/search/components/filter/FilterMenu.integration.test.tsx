import { afterEach, describe, expect, test } from "vitest";
import { FilterMenu } from "./FilterMenu";
import { cleanup, fireEvent, render, screen } from "@testing-library/react";
import React from "react";
import { MimirorgThemeProvider } from "@mimirorg/component-library";
import "@testing-library/jest-dom";


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
  const filterButton = screen.getByRole('button', {name: "Filter"});

  return {
    mockToggleFilter,
    filterButton,
    ...testComponent
  };
};

describe("Filter Button tests", () => {
  afterEach(() => {
    cleanup();
  });

  test("Should display filter dropdown menu", async () => {
    const { filterButton } = setup();
    fireEvent.click(filterButton);

    expect(await screen.findByText("Entity")).toBeVisible();
  });

  test("Should expand filters available on each menu item", async () => {
    const { filterButton } = setup();
    fireEvent.click(filterButton);

    const accordionButton = screen.getByText("Entity");
    fireEvent.click(accordionButton);
    expect(await screen.findByText("Block")).toBeVisible(); //toBeVisible
  });

  test("Toggle filter click handler called", async ()  => {
    const { filterButton, mockToggleFilter } = setup();
    fireEvent.click(filterButton); //bruke userEvent?

    const accordionButton = screen.getByText("Entity");
    fireEvent.click(accordionButton);

    const availableFiltersButton = screen.getByText("Block");
    fireEvent.click(availableFiltersButton);
    expect(mockToggleFilter).toHaveBeenCalled();
  });
});