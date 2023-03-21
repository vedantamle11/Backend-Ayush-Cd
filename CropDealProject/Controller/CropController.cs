﻿using CropDealProject.Models;
using CropDealProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CropDealProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CropController : ControllerBase
    {
        public readonly CropService _cropService;
        public CropController(CropService cropService)
        {
            _cropService = cropService;
        }


        #region GetAll
        // [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var crops = await _cropService.GetAll();
            return Ok(crops);


        }
        #endregion


        #region GetById
        [HttpGet("{id:int}")]

        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var crops = await _cropService.GetById(id);


            if (crops != null)
            {
                return Ok(crops);
            }
            return NotFound();



        }
        #endregion

        #region Insert
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Insert([Bind()] Crop entity)
        {
            try
            {
                await _cropService.Insert(entity);
                await _cropService.Save();
                return (Ok());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Edit
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] Crop crop)
        {

            await _cropService.Edit(crop);
            await _cropService.Save();
            return Ok(crop);



        }
        #endregion

        #region Delete
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            await _cropService.Delete(id);
            return Ok();

        }
        #endregion

    }
}
